import { Image } from '@chakra-ui/image'
import { Box, Flex, Link } from '@chakra-ui/layout'
import { ChakraProps } from '@chakra-ui/system'
import { NavLink as RouterNavLink } from 'react-router-dom'

import './ServerAvatar.scss'

type ServerAvatarProps = {
  to?: string
  avatarUrl: string
} & ChakraProps

export function UserAvatar ({ to, avatarUrl, ...rest }: ServerAvatarProps): JSX.Element {
  if (to) {
    <Box {...rest}>
      <Link as={RouterNavLink} to={to} activeClassName="active">
        <Flex bg="white" w="45px" h="45px" rounded="lg" align="center" justify="center">
          <Image objectFit="cover" objectPosition="center" src={avatarUrl} w="45px" h="45px" rounded="lg" />
        </Flex>
      </Link>
    </Box>
  }

  return (
    <Box {...rest}>
      <Flex>
        <Flex bg="white" w="50px" h="50px" rounded="lg" align="center" justify="center">
          <Image objectFit="cover" objectPosition="center" src={avatarUrl} w="50px" h="50px" rounded="lg" />
        </Flex>
      </Flex>
    </Box>
  )
}
