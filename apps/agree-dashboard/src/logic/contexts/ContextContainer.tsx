import { PropsWithChildren } from 'react'
import { resource } from '../../i18n'
import { I18nProvider } from '../../presentation/contexts/I18nContext'
import { AxiosHttpClient } from '../services/implementations/AxiosHttpClient'
import { ConsoleLogger } from '../services/implementations/ConsoleLogger'
import { LocalStorageCache } from '../services/implementations/LocalStorageCache'
import { AuthProvider } from './AuthContext'

const axiosHttpClient = new AxiosHttpClient()
const localStorageCache = new LocalStorageCache()
const consoleLogger = new ConsoleLogger()

export function ContextContainer ({ children }: PropsWithChildren<any>): JSX.Element {
  return (
    <I18nProvider logger={consoleLogger} resource={resource}>
      <AuthProvider cache={localStorageCache} httpClient={axiosHttpClient} logger={consoleLogger}>
        {children}
      </AuthProvider>
    </I18nProvider>
  )
}
