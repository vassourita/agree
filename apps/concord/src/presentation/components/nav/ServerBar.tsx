import { Box, Flex, List } from '@chakra-ui/layout'
import { FiCompass, FiPlus, FiSettings } from 'react-icons/fi'

import IconImg from '../../assets/iconw.svg'
import { ServerAvatar } from '../avatar/ServerAvatar'

export function ServerBar (): JSX.Element {
  return (
    <Flex flexDirection="column" align="center" justify="space-between" position="relative" h="100vh" minW="100px" bg="brand.600" roundedRight="md">

      <Flex flexDirection="column" align="center" justify="center" minW="100px" marginTop="1.75rem" marginBottom="1.25rem">

        <ServerAvatar to="/home" variant="fill" icon={IconImg} marginBottom="1.75rem" />

        <Box border="1px solid white" width="75px" />

      </Flex>

      <List w="full" h="full" className="hide-scrollbar" overflowY="auto" display="flex" flexDirection="column" alignItems="center" justifyContent="flex-start">

        {Array.from(Array(6).keys()).map(i => (
          <ServerAvatar key={i} variant="solid" icon="https://source.unsplash.com/random" to={`/s/${i}`}/>
        ))}

        <ServerAvatar to="/s/new" variant="dashed" icon={<FiPlus color="white" size={24} />} marginY="0.5rem" />

        <ServerAvatar to="/s/search" variant="dashed" icon={<FiCompass color="white" size={24} />} marginY="0.5rem" />

      </List>

      <Flex flexDirection="column" align="center" justify="center" minW="100px" marginBottom="1.75rem" marginTop="1.25rem">

        <Box border="1px solid white" width="75px" />

        <ServerAvatar to="/settings" variant="solid" icon={<FiSettings color="white" size={24} />} marginTop="1.75rem" />

      </Flex>

    </Flex>
  )
}
