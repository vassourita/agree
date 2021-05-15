import { Flex, Heading } from '@chakra-ui/layout'
import { useLocation } from 'react-router'
import { useAllow } from '../../../logic/hooks/useAllow'
import { Button } from '../../components/form/Button'

export function HomePage () : JSX.Element {
  const allow = useAllow()

  const location = useLocation()

  return (
    <Flex flexDirection="column">
      <Heading>{location.pathname}</Heading>
      <Button p="1rem" onClick={() => allow.logout()}>logout</Button>
    </Flex>
  )
}
