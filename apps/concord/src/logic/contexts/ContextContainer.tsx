import { PropsWithChildren } from 'react'
import { resource } from '../../i18n'
import { I18nProvider } from '../../presentation/contexts/I18nContext'
import { AxiosHttpClient } from '../services/implementations/AxiosHttpClient'
import { ConsoleLogger } from '../services/implementations/ConsoleLogger'
import { LocalStorageCache } from '../services/implementations/LocalStorageCache'
import { AllowProvider } from './AllowContext'

const axiosHttpClient = new AxiosHttpClient()
const localStorageCache = new LocalStorageCache()
const consoleLogger = new ConsoleLogger()

export function ContextContainer ({ children }: PropsWithChildren<any>): JSX.Element {
  return (
    <I18nProvider resource={resource}>
      <AllowProvider cache={localStorageCache} httpClient={axiosHttpClient} logger={consoleLogger}>
        {children}
      </AllowProvider>
    </I18nProvider>
  )
}
