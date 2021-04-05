import { ChakraProvider } from '@chakra-ui/react'
import { BrowserRouter } from 'react-router-dom'

import { theme } from './styles/theme'
import { Routes } from './Routes'
import { ContextContainer } from '../logic/contexts/ContextContainer'

export function App (): JSX.Element {
  return (
    <BrowserRouter>
      <ChakraProvider theme={theme} resetCSS>
        <ContextContainer>
          <Routes />
        </ContextContainer>
      </ChakraProvider>
    </BrowserRouter>
  )
}
