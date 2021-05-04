import { ChakraProvider } from '@chakra-ui/react'
import { BrowserRouter } from 'react-router-dom'

import { theme } from './styles/theme'
import { Routes } from './Routes'
import { ContextContainer } from '../logic/contexts/ContextContainer'

import './styles/utils.scss'

const baseUrl = document.getElementsByTagName('base')?.[0]?.getAttribute('href')

export function App (): JSX.Element {
  return (
    <BrowserRouter basename={baseUrl || undefined}>
      <ChakraProvider theme={theme} resetCSS>
        <ContextContainer>
          <Routes />
        </ContextContainer>
      </ChakraProvider>
    </BrowserRouter>
  )
}
