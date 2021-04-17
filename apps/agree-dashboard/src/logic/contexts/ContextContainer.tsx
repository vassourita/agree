import { PropsWithChildren } from 'react'
import { AxiosHttpClient } from '../services/implementations/AxiosHttpClient'
import { ConsoleLogger } from '../services/implementations/ConsoleLogger'
import { LocalStorageCache } from '../services/implementations/LocalStorageCache'
import { AuthProvider } from './AuthContext'

const axiosHttpClient = new AxiosHttpClient()
const localStorageCache = new LocalStorageCache()
const consoleLogger = new ConsoleLogger()

export function ContextContainer ({ children }: PropsWithChildren<any>): JSX.Element {
  return (
    <AuthProvider cache={localStorageCache} httpClient={axiosHttpClient} logger={consoleLogger}>
      {children}
    </AuthProvider>
  )
}
