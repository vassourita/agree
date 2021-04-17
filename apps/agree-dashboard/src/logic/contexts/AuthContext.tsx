import { useToast } from '@chakra-ui/toast'
import { createContext, ReactNode, useEffect, useState } from 'react'
import { useHistory } from 'react-router'
import { Account } from '../models/Account'
import { ICache } from '../services/ICache'
import { HttpStatusCode, IHttpClient } from '../services/IHttpClient'
import { ILogger } from '../services/ILogger'

export type AuthContextProps = {
  isAuthenticated: boolean
  account: Account | null
  accessToken: string | null
  refreshToken: string | null
  login(email: string, password: string): void
  logout(): void
  register(userName: string, email: string, password: string): void
}

export const AuthContext = createContext<AuthContextProps>({} as AuthContextProps)

export type AuthProviderProps = {
  httpClient: IHttpClient
  cache: ICache
  logger: ILogger
  children: ReactNode
}

const ACCESS_TOKEN_KEY = '@agree/access-token'
const REFRESH_TOKEN_KEY = '@agree/refresh-token'

export function AuthProvider ({ httpClient, cache, children, logger }: AuthProviderProps): JSX.Element {
  const [account, setAccount] = useState<Account | null>(null)
  const [accessToken, setAccessToken] = useState<string | null>(cache.get<string>(ACCESS_TOKEN_KEY)?.replace('"', '') || null)
  const [refreshToken, setRefreshToken] = useState<string | null>(cache.get<string>(REFRESH_TOKEN_KEY)?.replace('"', '') || null)

  const toast = useToast()
  const history = useHistory()

  async function me (): Promise<Account | null> {
    const response = await httpClient.request({
      method: 'get',
      headers: {
        Authorization: `Bearer ${accessToken}`
      },
      url: `${process.env.REACT_APP_API_URL}/accounts/@me`
    })

    if (response.statusCode !== HttpStatusCode.OK) {
      setAccount(null)
      return null
    }
    setAccount(response.body.user)
    return response.body.user
  }

  useEffect(() => {
    if (accessToken) {
      cache.set(ACCESS_TOKEN_KEY, accessToken)
    } else {
      cache.delete(ACCESS_TOKEN_KEY)
    }
    me().then(acc => {
      toast({
        title: `Bem vindo, ${acc?.userName}#${acc?.tag}!`,
        isClosable: true,
        status: 'success'
      })
    })
  }, [accessToken])

  useEffect(() => {
    if (refreshToken) {
      cache.set(REFRESH_TOKEN_KEY, refreshToken)
    } else {
      cache.delete(REFRESH_TOKEN_KEY)
    }
  }, [refreshToken])

  async function login (email: string, password: string) {
    const response = await httpClient.request({
      method: 'post',
      body: {
        email,
        password,
        grantType: 'password'
      },
      url: `${process.env.REACT_APP_API_URL}/accounts/login`
    })

    if (response.statusCode === HttpStatusCode.OK) {
      setRefreshToken(response.body.refreshToken)
      setAccessToken(response.body.accessToken)
      logger.info(response.body)
      history.push('/')
    } else {
      setRefreshToken(null)
      setAccessToken(null)
      logger.info(response.body)
      toast({
        title: response.body.message,
        isClosable: true,
        status: 'error'
      })
    }
  }

  async function register (userName: string, email: string, password: string) {
    const response = await httpClient.request({
      method: 'post',
      body: {
        email,
        password,
        userName
      },
      url: `${process.env.REACT_APP_API_URL}/accounts/register`
    })

    if (response.statusCode === HttpStatusCode.OK) {
      logger.info(response.body)
      toast({
        title: response.body.message,
        description: `Enviamos um email de confirmação para ${email}. Você precisa confirmar seu email antes de fazer login.`,
        isClosable: true,
        status: 'success'
      })
    } else {
      logger.info(response.body)
      toast({
        title: response.body.message,
        isClosable: true,
        status: 'error'
      })
    }
  }

  function logout () {
    setRefreshToken(null)
    setAccessToken(null)
    setAccount(null)
    history.push('/login')
  }

  return (
    <AuthContext.Provider value={{ account, isAuthenticated: !!account, login, logout, accessToken, refreshToken, register }}>
      {children}
    </AuthContext.Provider>
  )
}
