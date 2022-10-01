import { Box, Divider, Flex, Heading, Link, List, ListItem, Text } from '@chakra-ui/layout'
import { NavLink as RouterNavLink, Switch } from 'react-router-dom'
import { AuthenticatedRoute } from '../../logic/hooks/useAllow'
import { AppearanceSettingsPage } from '../pages/settings/AppearanceSettingsPage'
import { ProfileSettingsPage } from '../pages/settings/ProfileSettingsPage'

import './SettingsLayout.scss'

export function SettingsLayout (): JSX.Element {
  return (
    <Flex flexDir="column" w="100%" py="2rem" px="3rem" className="SettingsLayout">
      <Flex w="full" justify="center" direction="column">
        <Box>
          <Text fontWeight="semibold">Ah, então você é desses que gosta de customizar tudo?</Text>
          <Heading as="h1" fontSize="2.5rem" mb="2.5rem">
            Configurações
          </Heading>
        </Box>

        <Flex>
          <List>
            <ListItem w="full" className="SettingsLayoutNavLinkContainer" mb="1rem">
              <Link rounded="md" w="full" fontSize="1.3rem" as={RouterNavLink} to="/settings/profile" px="0.9rem" py="0.5rem" activeClassName="active">
                Perfil
              </Link>
            </ListItem>
            <ListItem w="full" className="SettingsLayoutNavLinkContainer" mb="1rem">
              <Link rounded="md" w="full" fontSize="1.3rem" as={RouterNavLink} to="/settings/appearance" px="0.9rem" py="0.5rem" activeClassName="active">
                Aparência
              </Link>
            </ListItem>
            <ListItem w="full" className="SettingsLayoutNavLinkContainer">
              <Link rounded="md" w="full" fontSize="1.3rem" as={RouterNavLink} to="/settings/voiceandvideo" px="0.9rem" py="0.5rem" activeClassName="active">
                Voz e Vídeo
              </Link>
            </ListItem>
          </List>

          <Divider orientation="vertical" mx="2rem" />

          <Flex>
            <Switch>
              <AuthenticatedRoute path="/settings/profile" component={() => <ProfileSettingsPage />}/>
              <AuthenticatedRoute path="/settings/appearance" component={() => <AppearanceSettingsPage />}/>
            </Switch>
          </Flex>
        </Flex>
      </Flex>
    </Flex>
  )
}
