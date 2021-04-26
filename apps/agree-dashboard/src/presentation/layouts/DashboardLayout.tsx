import { Flex, Heading } from '@chakra-ui/layout'
import { PropsWithChildren } from 'react'

export function DashboardLayout ({ children }: PropsWithChildren<unknown>): JSX.Element {
  return (
    <Flex flex="1">
      <Flex w="300px">
        <Heading>Sidebar</Heading>
      </Flex>
      <Flex w="100%">
        {children}
      </Flex>
    </Flex>
  )
}
