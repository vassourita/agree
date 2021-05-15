import { Flex } from '@chakra-ui/layout'
import { PropsWithChildren } from 'react'
import { ServerBar } from '../components/nav/ServerBar'
import { SideBar } from '../components/nav/SideBar'

export function DashboardLayout ({ children }: PropsWithChildren<unknown>): JSX.Element {
  return (
    <Flex h="100vh">
      <ServerBar />
      <SideBar />
      <Flex h="100vh" w="100%" bg="brand.900">
        {children}
      </Flex>
    </Flex>
  )
}
