import { Image } from '@chakra-ui/image'
import { Box, Flex, List, ListItem } from '@chakra-ui/layout'
import { FiCompass, FiPlus, FiSettings } from 'react-icons/fi'
import { Link } from 'react-router-dom'

import IconImg from '../../assets/iconw.svg'
import { ServerAvatar } from '../avatar/ServerAvatar'

export function ServerBar (): JSX.Element {
  return (
    <Flex flexDirection="column" align="center" justify="space-between" position="relative" h="100vh" minW="100px" bg="brand.600" roundedRight="md">

      <Flex flexDirection="column" align="center" justify="center" minW="100px" marginY="1.75rem">

        <Flex marginBottom="1.75rem" >
          <Link to="/">
            <Flex bg="white" w="50px" h="50px" rounded="md">
              <Image src={IconImg} />
            </Flex>
          </Link>
        </Flex>

        <Box border="1px solid white" width="75px" />
      </Flex>

      <List h="full" className="hide-scrollbar" overflowY="auto" display="flex" flexDirection="column" alignItems="center" justifyContent="flex-start">

        {Array.from(Array(5).keys()).map(i => (
          <ListItem key={i} marginBottom="1rem">
            <ServerAvatar url={IconImg} />
          </ListItem>
        ))}

        <ListItem marginBottom="1rem">
          <Link to="/">
            <Flex align="center" justify="center" bg="none" w="50px" h="50px" rounded="md" border="2px dashed white">
              <FiPlus size={24} color="white" />
            </Flex>
          </Link>
        </ListItem>

        <ListItem>
          <Link to="/">
            <Flex align="center" justify="center" bg="none" w="50px" h="50px" rounded="md" border="2px dashed white">
              <FiCompass size={24} color="white" />
            </Flex>
          </Link>
        </ListItem>

      </List>

      <Flex flexDirection="column" align="center" justify="center" minW="100px" marginY="1.75rem">

        <Box border="1px solid white" width="75px" />

        <Flex marginTop="1.75rem">
          <Link to="/">
            <Flex align="center" justify="center" bg="none" w="50px" h="50px" rounded="md" border="2px solid white">
              <FiSettings size={24} color="white" />
            </Flex>
          </Link>
        </Flex>

      </Flex>

    </Flex>
  )
}
