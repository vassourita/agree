import { Flex, Heading, Text } from '@chakra-ui/layout'

export function SettingsPage (): JSX.Element {
  return (
    <Flex flexDir="column" w="100%">
      <Flex fontWeight="bold" flexDir="column" p="3rem">
        <Text>Ah, então você é desses que gosta de personalizar tudo né?</Text>
        <Heading fontSize="5xl">Configurações</Heading>
      </Flex>
    </Flex>
  )
}
