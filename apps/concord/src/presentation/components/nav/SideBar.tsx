import { Box, Flex, Text } from '@chakra-ui/layout'
import { FiHeadphones, FiMic, FiSettings } from 'react-icons/fi'
import { useAllow } from '../../../logic/hooks/useAllow'
import { UserAvatar } from '../avatar/UserAvatar'
import { Button } from '../form/Button'

export function SideBar (): JSX.Element {
  const { account } = useAllow()

  return (
    <Flex justify="space-between" position="relative" h="100vh" minW="300px" bg="brand.700" flexDirection="column" >

      <Flex flexDirection="column" align="center" justify="center" minW="100px" marginTop="1.75rem" marginBottom="1.25rem">

      </Flex>

      <Flex h="full"></Flex>

      <Flex flexDirection="column" align="center" justify="center" minW="100px" marginBottom="1.75rem" marginTop="1.25rem">
        <Box border="1px solid white" width="250px" />

        <Flex w="250px" flexDirection="row" justify="space-between" align="center" marginTop="1.75rem">

          <Flex align="center">
            <UserAvatar to="/profile" avatarUrl="https://source.unsplash.com/random" />

            <Box ml="15px" fontWeight="medium">
              <Text overflow="hidden" whiteSpace="nowrap" maxW="100px" textColor="white" textOverflow="ellipsis">{account?.userName}</Text>
              <Text textColor="whiteAlpha.600">#{account?.tag}</Text>
            </Box>
          </Flex>

          <Flex align="center">
            <Button bg="none" p="0px" m="0px">
              <FiHeadphones size={21}/>
            </Button>

            <Button bg="none" p="0px" m="0px">
              <FiMic size={21}/>
            </Button>
          </Flex>

        </Flex>
      </Flex>

    </Flex>
  )
}
