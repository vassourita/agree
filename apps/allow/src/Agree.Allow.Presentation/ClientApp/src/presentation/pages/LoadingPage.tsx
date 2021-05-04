import { Image } from '@chakra-ui/image'
import { Flex, Heading, Text } from '@chakra-ui/layout'

import LogoImage from '../assets/agreew.svg'
import BgImage from '../assets/bg404.png'
import { useI18n } from '../hooks/useI18n'

type LoadingPageProps = {
  message: string
}

export function LoadingPage ({ message }: LoadingPageProps): JSX.Element {
  const { t } = useI18n()

  return (
    <Flex align="center" justify="center" h="100vh" flexDirection="column" bg={`url(${BgImage}) no-repeat`} bgSize="cover">
      <Image alt="Agree logo" h="4rem" src={LogoImage} mb="3rem" />
      <Heading as="h1" fontSize="4.2rem" mb="3rem">
        {t`Loading...`}
      </Heading>
      <Text fontSize="2rem">
        {t`${message}`}
      </Text>
    </Flex>
  )
}
