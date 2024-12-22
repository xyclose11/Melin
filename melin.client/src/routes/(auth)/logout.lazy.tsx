import { createLazyFileRoute } from '@tanstack/react-router'

export const Route = createLazyFileRoute('/(auth)/logout')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/logout"!</div>
}
