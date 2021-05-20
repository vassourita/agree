import { ChakraProvider } from '@chakra-ui/react'
import { PropsWithChildren } from 'react'
import { CookiesProvider } from 'react-cookie'
import { resource } from '../../i18n'
import { I18nProvider } from '../../presentation/contexts/I18nContext'
import { theme } from '../../presentation/styles/theme'
import { AxiosHttpClient } from '../services/implementations/AxiosHttpClient'
import { ConsoleLogger } from '../services/implementations/ConsoleLogger'
import { LocalStorageCache } from '../services/implementations/LocalStorageCache'
import { AllowProvider } from './AllowContext'

const axiosHttpClient = new AxiosHttpClient()
const localStorageCache = new LocalStorageCache()
const consoleLogger = new ConsoleLogger()

export function ContextContainer ({ children }: PropsWithChildren<any>): JSX.Element {
  return (
    <ChakraProvider theme={theme} resetCSS>
      <CookiesProvider>
        <I18nProvider resource={resource}>
          <AllowProvider cache={localStorageCache} httpClient={axiosHttpClient} logger={consoleLogger}>
            {children}
          </AllowProvider>
        </I18nProvider>
      </CookiesProvider>
    </ChakraProvider>
  )
}
