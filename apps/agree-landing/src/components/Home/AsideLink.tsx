import Link from 'next/link'
import { motion } from 'framer-motion'
import { ReactNode } from 'react'
import { IconType } from 'react-icons'

type AsideLinkProps = {
  children?: ReactNode
  iconSize: number
  disabled: boolean
  href?: string
  icon: IconType
}

export function AsideLink ({ children, iconSize, disabled, icon: Icon, href }: AsideLinkProps) {
  return (
    <motion.li whileHover={{ scale: 1.03 }} className={disabled ? 'opacity-75 cursor-not-allowed w-full' : 'opacity-100 cursor-pointer w-full'}>
      <Link href={disabled ? '' : href}>
        <div className="bg-background p-3 sm:p-5 rounded-md text-primary max-w-full w-full">
          <Icon size={iconSize} className="mb-5" />
          <span className="text-lg sm:text-xl font-bold" >
            {children}
          </span>
        </div>
      </Link>
    </motion.li>
  )
}
