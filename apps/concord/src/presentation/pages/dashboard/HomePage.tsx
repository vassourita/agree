import { Flex, Heading } from '@chakra-ui/layout'
import { useAllow } from '../../../logic/hooks/useAllow'
import { Button } from '../../components/form/Button'

export function HomePage () : JSX.Element {
  const allow = useAllow()

  return (
    <Flex flexDirection="column">
      <Heading>HOME</Heading>
      <Button p="1rem" onClick={() => allow.logout()}>logout</Button>
    </Flex>
  )
}
