import { Image } from '@chakra-ui/image'
import { Box, Flex, ListItem } from '@chakra-ui/layout'
import { ChakraProps } from '@chakra-ui/system'
import { ReactNode } from 'react'
import { NavLink } from 'react-router-dom'

import './ServerAvatar.scss'

type ServerAvatarProps = {
  to: string
  icon: ReactNode
  variant: 'dashed' | 'fill' | 'solid'
} & ChakraProps

export function ServerAvatar ({ to, icon, variant, ...rest }: ServerAvatarProps): JSX.Element {
  if (typeof icon === 'string') {
    if (variant === 'fill') {
      return (
        <Box className="ServerAvatarContainer" {...rest}>
          <NavLink to={to} activeClassName="active">
            <Flex bg="white" w="50px" h="50px" rounded="lg" align="center" justify="center">
              <Image objectFit="cover" objectPosition="center" src={icon} w="50px" h="50px" rounded="lg" />
            </Flex>
          </NavLink>
        </Box>
      )
    }

    return (
      <ListItem className="ServerAvatarContainer" marginY="0.5rem" {...rest}>
        <NavLink to={to} activeClassName="active">
          <Flex bg="white" w="50px" h="50px" rounded="lg">
            <Image objectFit="cover" objectPosition="center" src={icon} w="50px" h="50px" rounded="lg" />
          </Flex>
        </NavLink>
      </ListItem>
    )
  }

  return (
    <Box className="ServerAvatarContainer" {...rest}>
      <NavLink to={to} activeClassName="active">
        <Flex border={`2px ${variant} white`} bg="none" w="50px" h="50px" rounded="lg" align="center" justify="center">
          {icon}
        </Flex>
      </NavLink>
    </Box>
  )
}