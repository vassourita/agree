import { useContext } from 'react'
import { ServerContext, ServerContextProps } from '../contexts/ServerContext'

export function useServers (): ServerContextProps {
  const ctx = useContext(ServerContext)

  return {
    ...ctx
  }
}
