import { Box, Flex, Heading, List, Text } from '@chakra-ui/layout'
import { Popover, PopoverBody, PopoverContent, PopoverHeader, PopoverTrigger } from '@chakra-ui/popover'
import { FiHeadphones, FiMic, FiPlus, FiSearch } from 'react-icons/fi'
import * as datefns from 'date-fns'

import { useAllow } from '../../../logic/hooks/useAllow'
import { useI18n } from '../../hooks/useI18n'
import { UserAvatar } from '../avatar/UserAvatar'
import { Button } from '../form/Button'
import { Input, InputGroup, InputRightElement } from '@chakra-ui/input'
import { IconButton } from '@chakra-ui/button'
import { FriendListItem } from '../avatar/FriendListItem'

export function SideBar (): JSX.Element {
  const { account, logout, resendConfirmationMail } = useAllow()
  const { t, dateFnsLocale } = useI18n()

  return (
    <Flex justify="space-between" position="relative" h="100vh" minW="300px" bg="brand.700" flexDir="column" >

      <Flex flexDir="column" align="center" justify="center" minW="100px" marginTop="1.75rem" marginBottom="0.5rem">
        <InputGroup h="50px" width="250px">
          <Input h="50px" width="250px" placeholder="Procurando algo?" borderColor="white" borderWidth="2px" _placeholder={{ color: 'white' }} />
          <InputRightElement h="50px"><FiSearch /></InputRightElement>
        </InputGroup>
      </Flex>

      <Flex flexDir="column" h="full" width="full" overflowY="auto" p="25px">

        <Flex align="center" justify="space-between" width="full" mb="1rem">
          <Heading fontSize="1.4rem" as="h6">SEUS AMIGOS</Heading>
          <IconButton border="2px" background="none" aria-label="Add friend" icon={<FiPlus size={22} />} />
        </Flex>

        <List w="full" h="full" className="custom-scrollbar ml" overflowY="auto" display="flex" flexDirection="column" alignItems="center" justifyContent="flex-start">

          {Array.from(Array(8).keys()).map(i => (
            <FriendListItem avatarUrl={`https://source.unsplash.com/random?${(i + 2) * 100}`} to={`/u/${i}`} key={i} />
          ))}

        </List>

      </Flex>

      <Flex flexDir="column" align="center" justify="center" minW="100px" marginBottom="1.75rem" marginTop="0.5rem">
        <Box border="1px solid white" width="250px" />

        <Flex w="250px" flexDir="row" justify="space-between" align="center" marginTop="1.75rem">

          <Flex align="center">
            <Popover
              placement="top-start"
              closeOnBlur
              closeOnEsc
            >
              <PopoverTrigger>
                <button>
                  <UserAvatar avatarUrl={'https://source.unsplash.com/random?me'} />
                </button>
              </PopoverTrigger>
              <PopoverContent bg="brand.900" w="250px">
                <PopoverHeader fontSize="sm" textAlign="center">{datefns.format(new Date(), 'PPPP', { locale: dateFnsLocale })}</PopoverHeader>
                <PopoverBody d="flex" flexDir="column" justifyContent="space-between">
                  <Button bg="none" p="0.6rem" onClick={() => logout()}>{t`Logout`}</Button>
                  {!account?.verified && <Button bg="none" p="0.6rem" onClick={() => resendConfirmationMail()}>{t`Resend confirmation mail`}</Button>}
                  <Button bg="none" p="0.6rem" disabled onClick={() => logout()}>{t`Forgot my password`}</Button>
                </PopoverBody>
              </PopoverContent>
            </Popover>

            <Box ml="0.7rem" fontWeight="medium">
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
