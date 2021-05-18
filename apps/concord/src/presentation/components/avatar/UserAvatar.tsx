import { Image } from '@chakra-ui/image'
import { Box, Flex, Link } from '@chakra-ui/layout'
import { ChakraProps } from '@chakra-ui/system'
import { NavLink as RouterNavLink } from 'react-router-dom'

type ServerAvatarProps = {
  w?: string
  avatarUrl: string
} & ChakraProps

export function UserAvatar ({ w, avatarUrl, ...rest }: ServerAvatarProps): JSX.Element {
  return (
    <Box {...rest}>
      <Flex>
        <Flex bg="white" w={w || '50px'} h={w || '50px'} rounded="lg" align="center" justify="center">
          <Image objectFit="cover" objectPosition="center" src={avatarUrl} w={w || '50px'} h={w || '50px'} rounded="lg" />
        </Flex>
      </Flex>
    </Box>
  )
}
