import { useToast } from '@chakra-ui/toast'
import { createContext, ReactNode, useState } from 'react'
import { Err, Ok, Result } from 'ts-results'
import { useI18n } from '../../presentation/hooks/useI18n'
import { AccordErrorList } from '../models/AccordErrorList'
import { Server } from '../models/Server'
// import { ServerPermissions } from '../models/ServerPermissions'
import { HttpStatusCode, IHttpClient } from '../services/IHttpClient'
import { ILogger } from '../services/ILogger'

export type ServerContextProps = {
  myServers: Server[]
  // myPermissions(serverId: string): Promise<ServerPermissions>
  searchServers(query: string, order: string, sort: string): Promise<Server[]>
  createServer(name: string, privacy: number, description?:string): Promise<Result<Server, AccordErrorList>>
}

export const ServerContext = createContext<ServerContextProps>({} as ServerContextProps)

export type AllowProviderProps = {
  httpClient: IHttpClient
  logger: ILogger
  children: ReactNode
}

export function ServerProvider ({ children, httpClient, logger }: AllowProviderProps): JSX.Element {
  const [myServers] = useState<Server[]>([])

  const { t } = useI18n()
  const toast = useToast()

  async function searchServers (query: string, order: string, sort: string): Promise<Server[]> {
    const response = await httpClient.request({
      method: 'get',
      url: `${process.env.REACT_APP_ACCORD_URL}/servers?sort_by=${sort}&order=${order}${query && `&q=${encodeURI(query)}`}`
    })

    if (response.statusCode === HttpStatusCode.OK) {
      return response.body.servers
    }

    toast({
      title: t`Um erro inesperado ocorreu`,
      isClosable: true,
      status: 'error'
    })
    return []
  }

  async function createServer (name: string, privacy: number, description?: string): Promise<Result<Server, AccordErrorList>> {
    const response = await httpClient.request({
      method: 'post',
      url: `${process.env.REACT_APP_ACCORD_URL}/servers`,
      body: {
        server: {
          name, privacy, description
        }
      }
    })

    logger.info(response)

    if (response.statusCode === HttpStatusCode.CREATED) {
      toast({
        title: t`Servidor criado!`,
        isClosable: true,
        status: 'success'
      })
      return Ok(response.body?.server)
    }

    toast({
      title: t`Um erro inesperado ocorreu`,
      isClosable: true,
      status: 'error'
    })
    return Err(response.body?.errors)
  }

  return (
    <ServerContext.Provider value={{ createServer, myServers, searchServers }}>
      {children}
    </ServerContext.Provider>
  )
}
