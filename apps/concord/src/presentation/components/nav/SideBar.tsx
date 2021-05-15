import { Box, Flex, Text } from '@chakra-ui/layout'
import { Popover, PopoverBody, PopoverContent, PopoverHeader, PopoverTrigger } from '@chakra-ui/popover'
import { FiHeadphones, FiMic } from 'react-icons/fi'
import * as datefns from 'date-fns'

import { useAllow } from '../../../logic/hooks/useAllow'
import { useI18n } from '../../hooks/useI18n'
import { UserAvatar } from '../avatar/UserAvatar'
import { Button } from '../form/Button'

export function SideBar (): JSX.Element {
  const { account, logout, resendConfirmationMail } = useAllow()
  const { t, dateFnsLocale } = useI18n()

  return (
    <Flex justify="space-between" position="relative" maxH="100vh" minW="300px" bg="brand.700" flexDirection="column" >

      <Flex flexDirection="column" align="center" justify="center" minW="100px" marginTop="1.75rem" marginBottom="1.25rem">

      </Flex>

      <Flex h="full"></Flex>

      <Flex flexDirection="column" align="center" justify="center" minW="100px" marginBottom="1.75rem" marginTop="1.25rem">
        <Box border="1px solid white" width="250px" />

        <Flex w="250px" flexDirection="row" justify="space-between" align="center" marginTop="1.75rem">

          <Flex align="center">
            <Popover
              placement="top-start"
              closeOnBlur
              closeOnEsc
            >
              <PopoverTrigger>
                <button>
                  <UserAvatar avatarUrl="https://source.unsplash.com/random" />
                </button>
              </PopoverTrigger>
              <PopoverContent bg="brand.900" w="250px">
                <PopoverHeader>{datefns.format(new Date(), 'PPPP', { locale: dateFnsLocale })}</PopoverHeader>
                <PopoverBody d="flex" flexDirection="column" justifyContent="space-between">
                  <Button bg="none" p="0.6rem" onClick={() => logout()}>{t`Logout`}</Button>
                  {!account?.verified && <Button bg="none" p="0.6rem" onClick={() => resendConfirmationMail()}>{t`Resend confirmation mail`}</Button>}
                  <Button bg="none" p="0.6rem" disabled onClick={() => logout()}>{t`Forgot my password`}</Button>
                </PopoverBody>
              </PopoverContent>
            </Popover>

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
