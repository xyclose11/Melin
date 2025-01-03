﻿import { createFileRoute, redirect } from '@tanstack/react-router'

export const Route = createFileRoute('/(auth)/_auth')({
  beforeLoad: async ({ context, location }) => {
    if (!context.auth.isAuthenticated) {
      throw redirect({
        to: '/login',
        search: {
          redirect: location.href,
        },
      })
    }
  },
})
