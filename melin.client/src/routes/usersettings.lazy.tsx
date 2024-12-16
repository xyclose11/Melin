import { createLazyFileRoute } from '@tanstack/react-router'

export const Route = createLazyFileRoute('/usersettings')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/usersettings"!</div>
}
