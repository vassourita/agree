import { ChakraProvider } from '@chakra-ui/react'

export function App (): JSX.Element {
  return (
    <ChakraProvider>
      <h1>Hello World</h1>
    </ChakraProvider>
  )
}
