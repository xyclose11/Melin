import { createFileRoute, redirect } from '@tanstack/react-router'
import { Roles, useAuth } from '@/utils/AuthProvider.tsx'

export const Route = createFileRoute('/(auth)/_auth/admin-dashboard')({
  component: AdminDashboard,
})

function AdminDashboard() {
  const auth = useAuth()
  if (auth.userRole !== Roles.Admin) {
    throw redirect({
      to: '/login',
      search: {
        redirect: location.href,
      },
    })
  }

  return <div>Hello "/admin-dashboard"!</div>
}
