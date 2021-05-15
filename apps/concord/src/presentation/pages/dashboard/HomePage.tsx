import { Flex, Heading } from '@chakra-ui/layout'
import { useLocation } from 'react-router'

export function HomePage () : JSX.Element {
  const location = useLocation()

  return (
    <Flex flexDirection="column">
      <Heading>{location.pathname}</Heading>
    </Flex>
  )
}
