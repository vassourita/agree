import { motion } from 'framer-motion'
import { IconType } from 'react-icons/lib'

type NavLinkProps = {
  icon: IconType
  iconSize: number
}

export function NavLink ({ iconSize, icon: Icon }: NavLinkProps) {
  return (
    <motion.li whileHover={{ scale: 1.1 }}>
      <Icon size={iconSize} className="cursor-help" />
    </motion.li>
  )
}
