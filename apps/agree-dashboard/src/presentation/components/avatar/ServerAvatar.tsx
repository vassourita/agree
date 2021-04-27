import { Image } from '@chakra-ui/image'
import { Flex, ListItem } from '@chakra-ui/layout'
import { NavLink } from 'react-router-dom'

import './ServerAvatar.scss'

type ServerAvatarProps = {
  imgUrl: string
  to: string
}

export function ServerAvatar ({ imgUrl, to }: ServerAvatarProps): JSX.Element {
  return (
    <ListItem className="ServerAvatarContainer" marginY="0.5rem">
      <NavLink to={to} activeClassName="active">
        <Flex bg="white" w="50px" h="50px" rounded="lg">
          <Image src={imgUrl} />
        </Flex>
      </NavLink>
    </ListItem>
  )
}
