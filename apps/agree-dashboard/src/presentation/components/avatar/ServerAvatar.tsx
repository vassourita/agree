import { Image } from '@chakra-ui/image'
import { Flex } from '@chakra-ui/layout'
import { Link } from 'react-router-dom'

type ServerAvatarProps = {
  url: string
}

export function ServerAvatar ({ url }: ServerAvatarProps): JSX.Element {
  return (
    <Flex>
      <Link to="/">
        <Flex bg="white" w="50px" h="50px" rounded="md">
          <Image src={url} />
        </Flex>
      </Link>
    </Flex>
  )
}
