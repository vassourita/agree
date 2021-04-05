import { ReactNode } from 'react'
import { AxiosHttpClient } from '../services/implementations/AxiosHttpClient'
import { LocalStorageCache } from '../services/implementations/LocalStorageCache'
import { AuthProvider } from './AuthContext'

const axiosHttpClient = new AxiosHttpClient()
const localStorageCache = new LocalStorageCache()

type ContextContainerProps = {
  children: ReactNode
}

export function ContextContainer ({ children }: ContextContainerProps): JSX.Element {
  return (
    <AuthProvider cache={localStorageCache} httpClient={axiosHttpClient}>
      {children}
    </AuthProvider>
  )
}
