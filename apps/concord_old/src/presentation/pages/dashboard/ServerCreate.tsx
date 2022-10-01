import { Flex, Heading } from '@chakra-ui/layout'

export function ServerCreate (): JSX.Element {
  return (
    <Flex flexDir="column" w="100%" pt="3rem" px="3rem">
      <Heading fontWeight="extrabold" fontSize="5xl" as="h1" mb="2rem">
        Criar novo servidor
      </Heading>
    </Flex>
  )
}
