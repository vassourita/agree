import { PropsWithChildren } from 'react'
import { AxiosHttpClient } from '../services/implementations/AxiosHttpClient'
import { LocalStorageCache } from '../services/implementations/LocalStorageCache'
import { AuthProvider } from './AuthContext'

const axiosHttpClient = new AxiosHttpClient()
const localStorageCache = new LocalStorageCache()

export function ContextContainer ({ children }: PropsWithChildren<any>): JSX.Element {
  return (
    <AuthProvider cache={localStorageCache} httpClient={axiosHttpClient}>
      {children}
    </AuthProvider>
  )
}
