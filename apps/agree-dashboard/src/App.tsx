import { ChakraProvider, Heading } from '@chakra-ui/react'
import { theme } from './styles/theme'

export function App (): JSX.Element {
  return (
    <ChakraProvider theme={theme} resetCSS>
      <Heading size="4xl">Hello Agree!</Heading>
    </ChakraProvider>
  )
}
