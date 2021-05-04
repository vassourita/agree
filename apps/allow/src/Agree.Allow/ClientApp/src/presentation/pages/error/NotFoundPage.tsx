import { Image } from '@chakra-ui/image'
import { Flex, Heading, Text } from '@chakra-ui/layout'
import { useI18n } from '../../hooks/useI18n'

import LogoImage from '../../assets/agreew.svg'
import BgImage from '../../assets/bg404.png'

export function NotFoundPage (): JSX.Element {
  const { t } = useI18n()

  return (
    <Flex align="center" justify="center" h="100vh" flexDirection="column" bg={`url(${BgImage}) no-repeat`} bgSize="cover">
      <Image alt="Agree logo" h="4rem" src={LogoImage} mb="3rem" />
      <Heading as="h1" fontSize="4.2rem" mb="3rem">
        404 NOT FOUND
      </Heading>
      <Text fontSize="2rem">
        {t`Congratulations! You found our little mascot secret page!`}
      </Text>
      <Text fontSize="2rem" mb="3rem">
        {t`Just kidding. Actually this page doesn't exists...`}
      </Text>
      <Text>
        {t`Kinda paradoxal don't you think?`}
      </Text>
    </Flex>
  )
}
