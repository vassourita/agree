import Icon from '@chakra-ui/icon'
import { Image } from '@chakra-ui/image'
import { Input, InputGroup, InputLeftAddon } from '@chakra-ui/input'
import { Box, Flex, Grid, Heading, Text } from '@chakra-ui/layout'
import { Select } from '@chakra-ui/select'
import { FiChevronDown, FiFilter, FiSearch } from 'react-icons/fi'

export function ServerSearch (): JSX.Element {
  return (
    <Flex flexDir="column" w="100%" pt="3rem" px="3rem">
      <Heading fontWeight="extrabold" fontSize="5xl" as="h1" mb="2rem">
        Buscar servidores
      </Heading>
      <Grid gridTemplateColumns="repeat(3, 1fr)" gridTemplateAreas="'search search sort'" gridGap="3rem" mb="3.2rem">
        <InputGroup h="50px" width="100%" gridArea="search">
          <InputLeftAddon h="50px" bg="white">
            <Icon color="gray.600" as={FiSearch} />
          </InputLeftAddon>
          <Input h="50px" width="100%" placeholder="Busque por nome, descrição ou ID" borderColor="white" borderWidth="2px" _placeholder={{ color: 'white' }} />
        </InputGroup>
        <InputGroup>
          <InputLeftAddon h="50px" bg="white">
            <Icon color="gray.600" as={FiFilter} />
          </InputLeftAddon>
          <Select roundedLeft="0" icon={<FiChevronDown />} gridArea="sort" h="50px" width="100%" borderColor="white" borderWidth="2px" _placeholder={{ color: 'white' }}>
            <option value="name|asc">Nome (A-Z)</option>
            <option value="name|desc">Nome (Z-A)</option>
            <option value="description|asc">Descrição (A-Z)</option>
            <option value="description|desc">Descrição (Z-A)</option>
            <option value="created_at|asc">Mais antigos</option>
            <option value="created_at|desc">Mais novos</option>
          </Select>
        </InputGroup>
      </Grid>
      <Grid gridTemplateColumns="repeat(3, 1fr)" gridAutoRows="350px" gridGap="3rem" overflowY="scroll" className="hide-scrollbar">
        {Array.from(Array(12).keys()).map(i => (
          <Flex position="relative" direction="column" key={i} bg="#49494f" w="full" h="350px" rounded="md">
            <Image filter="brightness(35%)" objectFit="cover" objectPosition="center" h="8rem" w="full" rounded="md" src={'https://source.unsplash.com/random'}/>
            <Text position="absolute" top="0.5rem" left="0.7rem" fontSize="xl">TItulooo</Text>
            <Flex direction="column" h="full" p="1rem" justifyContent="space-between">
              <Text>Por algum motivo esse nome me lembra goiaba. Eu gosto de goiaba. Mas as sementes são meio ruins de morder e tal.</Text>
              <Box>
                <Text>11 membros</Text>
                <Text>Servidor público</Text>
              </Box>
            </Flex>
          </Flex>
        ))}
      </Grid>
    </Flex>
  )
}
