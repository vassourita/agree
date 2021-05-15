import { Image } from '@chakra-ui/image'
import { Box, Flex, ListItem, Link } from '@chakra-ui/layout'
import { ChakraProps } from '@chakra-ui/system'
import { Tooltip } from '@chakra-ui/tooltip'
import { ReactNode } from 'react'
import { NavLink as RouterNavLink } from 'react-router-dom'

import './ServerAvatar.scss'

type ServerAvatarProps = {
  to: string
  icon: ReactNode
  tooltip?: string
  variant: 'dashed' | 'fill' | 'solid'
} & ChakraProps

export function ServerAvatar ({ to, icon, variant, tooltip, ...rest }: ServerAvatarProps): JSX.Element {
  if (typeof icon === 'string') {
    if (variant === 'fill') {
      return (
        <Box className="ServerAvatarContainer" {...rest}>
          <Link as={RouterNavLink} to={to} activeClassName="active">
            <Tooltip label={tooltip || to} fontSize="md" placement="right">
              <Flex bg="white" w="50px" h="50px" rounded="lg" align="center" justify="center">
                <Image objectFit="cover" objectPosition="center" src={icon} w="50px" h="50px" rounded="lg" />
              </Flex>
            </Tooltip>
          </Link>
        </Box>
      )
    }

    return (
      <ListItem className="ServerAvatarContainer" marginY="0.5rem" {...rest}>
        <Link as={RouterNavLink} to={to} activeClassName="active">
          <Tooltip label={tooltip || to} fontSize="md" placement="right">
            <Flex bg="white" w="50px" h="50px" rounded="lg">
              <Image objectFit="cover" objectPosition="center" src={icon} w="50px" h="50px" rounded="lg" />
            </Flex>
          </Tooltip>
        </Link>
      </ListItem>
    )
  }

  return (
    <Box className="ServerAvatarContainer" {...rest}>
      <Link as={RouterNavLink} to={to} activeClassName="active">
        <Tooltip label={tooltip || to} fontSize="md" placement="right">
          <Flex border={`2px ${variant} white`} bg="none" w="50px" h="50px" rounded="lg" align="center" justify="center">
            {icon}
          </Flex>
        </Tooltip>
      </Link>
    </Box>
  )
}
