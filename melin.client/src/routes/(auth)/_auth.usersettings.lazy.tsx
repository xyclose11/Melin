import { createLazyFileRoute } from '@tanstack/react-router'
import UserSettingsPage from '@/UserSettingsPage.tsx'

export const Route = createLazyFileRoute('/(auth)/_auth/usersettings')({
  component: UserSettingsPage,
})
