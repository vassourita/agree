import { Box, Link, ListItem, Text } from '@chakra-ui/layout'
import { UserAvatar } from './UserAvatar'
import { NavLink as RouterNavLink } from 'react-router-dom'

import './FriendListItem.scss'

export type FriendListItemProps = {
  to: string
  avatarUrl: string
}

export function FriendListItem ({ to, avatarUrl }: FriendListItemProps): JSX.Element {
  return (
  <ListItem d="flex" w="full" mb="0.8rem" alignItems="center" _last={{ mb: '0' }} pr="0.5rem">
    <Link className="FriendListItemContainer" d="flex" w="full" as={RouterNavLink} _focus={{ border: 'transparent' }} to={to} rounded="md" activeClassName="active" _hover={{ textDecoration: 'none', backgroundColor: 'brand.600' }}>
      <UserAvatar avatarUrl={avatarUrl}/>
      <Box ml="0.7rem">
        <Text overflow="hidden" whiteSpace="nowrap" maxW="50rem" textOverflow="ellipsis">MyFriend</Text>
        <Text textColor="whiteAlpha.800" fontSize="sm">Online</Text>
      </Box>
    </Link>
  </ListItem>
  )
}
