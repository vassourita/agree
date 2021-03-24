import { ChakraProvider } from '@chakra-ui/react'
import { theme } from './styles/theme'
import { BrowserRouter } from 'react-router-dom'
import { Routes } from './Routes'

export function App (): JSX.Element {
  return (
    <BrowserRouter>
      <ChakraProvider theme={theme} resetCSS>
        <Routes />
      </ChakraProvider>
    </BrowserRouter>
  )
}
