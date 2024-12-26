/* eslint-disable */

// @ts-nocheck

// noinspection JSUnusedGlobalSymbols

// This file was automatically generated by TanStack Router.
// You should NOT make any changes in this file as it will be overwritten.
// Additionally, you should also exclude this file from your linter and/or formatter to prevent it from being checked or modified.

import { createFileRoute } from '@tanstack/react-router'

// Import Routes

import { Route as rootRoute } from './routes/__root'
import { Route as LibraryImport } from './routes/library'
import { Route as DocumentImport } from './routes/document'
import { Route as DocumentFileNameImport } from './routes/document.$fileName'
import { Route as authAuthImport } from './routes/(auth)/_auth'
import { Route as referenceEditReferenceRefIdImport } from './routes/(reference)/edit-reference.$refId'
import { Route as authAuthAdminDashboardImport } from './routes/(auth)/_auth.admin-dashboard'

// Create Virtual Routes

const authImport = createFileRoute('/(auth)')()
const ContactLazyImport = createFileRoute('/contact')()
const IndexLazyImport = createFileRoute('/')()
const teamCreateTeamLazyImport = createFileRoute('/(team)/create-team')()
const referenceCreateReferenceLazyImport = createFileRoute(
  '/(reference)/create-reference',
)()
const authUsersettingsLazyImport = createFileRoute('/(auth)/usersettings')()
const authSignupLazyImport = createFileRoute('/(auth)/signup')()
const authResetPasswordLazyImport = createFileRoute('/(auth)/reset-password')()
const authLogoutLazyImport = createFileRoute('/(auth)/logout')()
const authLoginLazyImport = createFileRoute('/(auth)/login')()

// Create/Update Routes

const authRoute = authImport.update({
  id: '/(auth)',
  getParentRoute: () => rootRoute,
} as any)

const ContactLazyRoute = ContactLazyImport.update({
  id: '/contact',
  path: '/contact',
  getParentRoute: () => rootRoute,
} as any).lazy(() => import('./routes/contact.lazy').then((d) => d.Route))

const LibraryRoute = LibraryImport.update({
  id: '/library',
  path: '/library',
  getParentRoute: () => rootRoute,
} as any)

const DocumentRoute = DocumentImport.update({
  id: '/document',
  path: '/document',
  getParentRoute: () => rootRoute,
} as any)

const IndexLazyRoute = IndexLazyImport.update({
  id: '/',
  path: '/',
  getParentRoute: () => rootRoute,
} as any).lazy(() => import('./routes/index.lazy').then((d) => d.Route))

const teamCreateTeamLazyRoute = teamCreateTeamLazyImport
  .update({
    id: '/(team)/create-team',
    path: '/create-team',
    getParentRoute: () => rootRoute,
  } as any)
  .lazy(() => import('./routes/(team)/create-team.lazy').then((d) => d.Route))

const referenceCreateReferenceLazyRoute = referenceCreateReferenceLazyImport
  .update({
    id: '/(reference)/create-reference',
    path: '/create-reference',
    getParentRoute: () => rootRoute,
  } as any)
  .lazy(() =>
    import('./routes/(reference)/create-reference.lazy').then((d) => d.Route),
  )

const authUsersettingsLazyRoute = authUsersettingsLazyImport
  .update({
    id: '/usersettings',
    path: '/usersettings',
    getParentRoute: () => authRoute,
  } as any)
  .lazy(() => import('./routes/(auth)/usersettings.lazy').then((d) => d.Route))

const authSignupLazyRoute = authSignupLazyImport
  .update({
    id: '/signup',
    path: '/signup',
    getParentRoute: () => authRoute,
  } as any)
  .lazy(() => import('./routes/(auth)/signup.lazy').then((d) => d.Route))

const authResetPasswordLazyRoute = authResetPasswordLazyImport
  .update({
    id: '/reset-password',
    path: '/reset-password',
    getParentRoute: () => authRoute,
  } as any)
  .lazy(() =>
    import('./routes/(auth)/reset-password.lazy').then((d) => d.Route),
  )

const authLogoutLazyRoute = authLogoutLazyImport
  .update({
    id: '/logout',
    path: '/logout',
    getParentRoute: () => authRoute,
  } as any)
  .lazy(() => import('./routes/(auth)/logout.lazy').then((d) => d.Route))

const authLoginLazyRoute = authLoginLazyImport
  .update({
    id: '/login',
    path: '/login',
    getParentRoute: () => authRoute,
  } as any)
  .lazy(() => import('./routes/(auth)/login.lazy').then((d) => d.Route))

const DocumentFileNameRoute = DocumentFileNameImport.update({
  id: '/$fileName',
  path: '/$fileName',
  getParentRoute: () => DocumentRoute,
} as any)

const authAuthRoute = authAuthImport.update({
  id: '/_auth',
  getParentRoute: () => authRoute,
} as any)

const referenceEditReferenceRefIdRoute =
  referenceEditReferenceRefIdImport.update({
    id: '/(reference)/edit-reference/$refId',
    path: '/edit-reference/$refId',
    getParentRoute: () => rootRoute,
  } as any)

const authAuthAdminDashboardRoute = authAuthAdminDashboardImport.update({
  id: '/admin-dashboard',
  path: '/admin-dashboard',
  getParentRoute: () => authAuthRoute,
} as any)

// Populate the FileRoutesByPath interface

declare module '@tanstack/react-router' {
  interface FileRoutesByPath {
    '/': {
      id: '/'
      path: '/'
      fullPath: '/'
      preLoaderRoute: typeof IndexLazyImport
      parentRoute: typeof rootRoute
    }
    '/document': {
      id: '/document'
      path: '/document'
      fullPath: '/document'
      preLoaderRoute: typeof DocumentImport
      parentRoute: typeof rootRoute
    }
    '/library': {
      id: '/library'
      path: '/library'
      fullPath: '/library'
      preLoaderRoute: typeof LibraryImport
      parentRoute: typeof rootRoute
    }
    '/contact': {
      id: '/contact'
      path: '/contact'
      fullPath: '/contact'
      preLoaderRoute: typeof ContactLazyImport
      parentRoute: typeof rootRoute
    }
    '/(auth)': {
      id: '/(auth)'
      path: '/'
      fullPath: '/'
      preLoaderRoute: typeof authImport
      parentRoute: typeof rootRoute
    }
    '/(auth)/_auth': {
      id: '/(auth)/_auth'
      path: '/'
      fullPath: '/'
      preLoaderRoute: typeof authAuthImport
      parentRoute: typeof authRoute
    }
    '/document/$fileName': {
      id: '/document/$fileName'
      path: '/$fileName'
      fullPath: '/document/$fileName'
      preLoaderRoute: typeof DocumentFileNameImport
      parentRoute: typeof DocumentImport
    }
    '/(auth)/login': {
      id: '/(auth)/login'
      path: '/login'
      fullPath: '/login'
      preLoaderRoute: typeof authLoginLazyImport
      parentRoute: typeof authImport
    }
    '/(auth)/logout': {
      id: '/(auth)/logout'
      path: '/logout'
      fullPath: '/logout'
      preLoaderRoute: typeof authLogoutLazyImport
      parentRoute: typeof authImport
    }
    '/(auth)/reset-password': {
      id: '/(auth)/reset-password'
      path: '/reset-password'
      fullPath: '/reset-password'
      preLoaderRoute: typeof authResetPasswordLazyImport
      parentRoute: typeof authImport
    }
    '/(auth)/signup': {
      id: '/(auth)/signup'
      path: '/signup'
      fullPath: '/signup'
      preLoaderRoute: typeof authSignupLazyImport
      parentRoute: typeof authImport
    }
    '/(auth)/usersettings': {
      id: '/(auth)/usersettings'
      path: '/usersettings'
      fullPath: '/usersettings'
      preLoaderRoute: typeof authUsersettingsLazyImport
      parentRoute: typeof authImport
    }
    '/(reference)/create-reference': {
      id: '/(reference)/create-reference'
      path: '/create-reference'
      fullPath: '/create-reference'
      preLoaderRoute: typeof referenceCreateReferenceLazyImport
      parentRoute: typeof rootRoute
    }
    '/(team)/create-team': {
      id: '/(team)/create-team'
      path: '/create-team'
      fullPath: '/create-team'
      preLoaderRoute: typeof teamCreateTeamLazyImport
      parentRoute: typeof rootRoute
    }
    '/(auth)/_auth/admin-dashboard': {
      id: '/(auth)/_auth/admin-dashboard'
      path: '/admin-dashboard'
      fullPath: '/admin-dashboard'
      preLoaderRoute: typeof authAuthAdminDashboardImport
      parentRoute: typeof authAuthImport
    }
    '/(reference)/edit-reference/$refId': {
      id: '/(reference)/edit-reference/$refId'
      path: '/edit-reference/$refId'
      fullPath: '/edit-reference/$refId'
      preLoaderRoute: typeof referenceEditReferenceRefIdImport
      parentRoute: typeof rootRoute
    }
  }
}

// Create and export the route tree

interface DocumentRouteChildren {
  DocumentFileNameRoute: typeof DocumentFileNameRoute
}

const DocumentRouteChildren: DocumentRouteChildren = {
  DocumentFileNameRoute: DocumentFileNameRoute,
}

const DocumentRouteWithChildren = DocumentRoute._addFileChildren(
  DocumentRouteChildren,
)

interface authAuthRouteChildren {
  authAuthAdminDashboardRoute: typeof authAuthAdminDashboardRoute
}

const authAuthRouteChildren: authAuthRouteChildren = {
  authAuthAdminDashboardRoute: authAuthAdminDashboardRoute,
}

const authAuthRouteWithChildren = authAuthRoute._addFileChildren(
  authAuthRouteChildren,
)

interface authRouteChildren {
  authAuthRoute: typeof authAuthRouteWithChildren
  authLoginLazyRoute: typeof authLoginLazyRoute
  authLogoutLazyRoute: typeof authLogoutLazyRoute
  authResetPasswordLazyRoute: typeof authResetPasswordLazyRoute
  authSignupLazyRoute: typeof authSignupLazyRoute
  authUsersettingsLazyRoute: typeof authUsersettingsLazyRoute
}

const authRouteChildren: authRouteChildren = {
  authAuthRoute: authAuthRouteWithChildren,
  authLoginLazyRoute: authLoginLazyRoute,
  authLogoutLazyRoute: authLogoutLazyRoute,
  authResetPasswordLazyRoute: authResetPasswordLazyRoute,
  authSignupLazyRoute: authSignupLazyRoute,
  authUsersettingsLazyRoute: authUsersettingsLazyRoute,
}

const authRouteWithChildren = authRoute._addFileChildren(authRouteChildren)

export interface FileRoutesByFullPath {
  '/': typeof authAuthRouteWithChildren
  '/document': typeof DocumentRouteWithChildren
  '/library': typeof LibraryRoute
  '/contact': typeof ContactLazyRoute
  '/document/$fileName': typeof DocumentFileNameRoute
  '/login': typeof authLoginLazyRoute
  '/logout': typeof authLogoutLazyRoute
  '/reset-password': typeof authResetPasswordLazyRoute
  '/signup': typeof authSignupLazyRoute
  '/usersettings': typeof authUsersettingsLazyRoute
  '/create-reference': typeof referenceCreateReferenceLazyRoute
  '/create-team': typeof teamCreateTeamLazyRoute
  '/admin-dashboard': typeof authAuthAdminDashboardRoute
  '/edit-reference/$refId': typeof referenceEditReferenceRefIdRoute
}

export interface FileRoutesByTo {
  '/': typeof authAuthRouteWithChildren
  '/document': typeof DocumentRouteWithChildren
  '/library': typeof LibraryRoute
  '/contact': typeof ContactLazyRoute
  '/document/$fileName': typeof DocumentFileNameRoute
  '/login': typeof authLoginLazyRoute
  '/logout': typeof authLogoutLazyRoute
  '/reset-password': typeof authResetPasswordLazyRoute
  '/signup': typeof authSignupLazyRoute
  '/usersettings': typeof authUsersettingsLazyRoute
  '/create-reference': typeof referenceCreateReferenceLazyRoute
  '/create-team': typeof teamCreateTeamLazyRoute
  '/admin-dashboard': typeof authAuthAdminDashboardRoute
  '/edit-reference/$refId': typeof referenceEditReferenceRefIdRoute
}

export interface FileRoutesById {
  __root__: typeof rootRoute
  '/': typeof IndexLazyRoute
  '/document': typeof DocumentRouteWithChildren
  '/library': typeof LibraryRoute
  '/contact': typeof ContactLazyRoute
  '/(auth)': typeof authRouteWithChildren
  '/(auth)/_auth': typeof authAuthRouteWithChildren
  '/document/$fileName': typeof DocumentFileNameRoute
  '/(auth)/login': typeof authLoginLazyRoute
  '/(auth)/logout': typeof authLogoutLazyRoute
  '/(auth)/reset-password': typeof authResetPasswordLazyRoute
  '/(auth)/signup': typeof authSignupLazyRoute
  '/(auth)/usersettings': typeof authUsersettingsLazyRoute
  '/(reference)/create-reference': typeof referenceCreateReferenceLazyRoute
  '/(team)/create-team': typeof teamCreateTeamLazyRoute
  '/(auth)/_auth/admin-dashboard': typeof authAuthAdminDashboardRoute
  '/(reference)/edit-reference/$refId': typeof referenceEditReferenceRefIdRoute
}

export interface FileRouteTypes {
  fileRoutesByFullPath: FileRoutesByFullPath
  fullPaths:
    | '/'
    | '/document'
    | '/library'
    | '/contact'
    | '/document/$fileName'
    | '/login'
    | '/logout'
    | '/reset-password'
    | '/signup'
    | '/usersettings'
    | '/create-reference'
    | '/create-team'
    | '/admin-dashboard'
    | '/edit-reference/$refId'
  fileRoutesByTo: FileRoutesByTo
  to:
    | '/'
    | '/document'
    | '/library'
    | '/contact'
    | '/document/$fileName'
    | '/login'
    | '/logout'
    | '/reset-password'
    | '/signup'
    | '/usersettings'
    | '/create-reference'
    | '/create-team'
    | '/admin-dashboard'
    | '/edit-reference/$refId'
  id:
    | '__root__'
    | '/'
    | '/document'
    | '/library'
    | '/contact'
    | '/(auth)'
    | '/(auth)/_auth'
    | '/document/$fileName'
    | '/(auth)/login'
    | '/(auth)/logout'
    | '/(auth)/reset-password'
    | '/(auth)/signup'
    | '/(auth)/usersettings'
    | '/(reference)/create-reference'
    | '/(team)/create-team'
    | '/(auth)/_auth/admin-dashboard'
    | '/(reference)/edit-reference/$refId'
  fileRoutesById: FileRoutesById
}

export interface RootRouteChildren {
  IndexLazyRoute: typeof IndexLazyRoute
  DocumentRoute: typeof DocumentRouteWithChildren
  LibraryRoute: typeof LibraryRoute
  ContactLazyRoute: typeof ContactLazyRoute
  authRoute: typeof authRouteWithChildren
  referenceCreateReferenceLazyRoute: typeof referenceCreateReferenceLazyRoute
  teamCreateTeamLazyRoute: typeof teamCreateTeamLazyRoute
  referenceEditReferenceRefIdRoute: typeof referenceEditReferenceRefIdRoute
}

const rootRouteChildren: RootRouteChildren = {
  IndexLazyRoute: IndexLazyRoute,
  DocumentRoute: DocumentRouteWithChildren,
  LibraryRoute: LibraryRoute,
  ContactLazyRoute: ContactLazyRoute,
  authRoute: authRouteWithChildren,
  referenceCreateReferenceLazyRoute: referenceCreateReferenceLazyRoute,
  teamCreateTeamLazyRoute: teamCreateTeamLazyRoute,
  referenceEditReferenceRefIdRoute: referenceEditReferenceRefIdRoute,
}

export const routeTree = rootRoute
  ._addFileChildren(rootRouteChildren)
  ._addFileTypes<FileRouteTypes>()

/* ROUTE_MANIFEST_START
{
  "routes": {
    "__root__": {
      "filePath": "__root.tsx",
      "children": [
        "/",
        "/document",
        "/library",
        "/contact",
        "/(auth)",
        "/(reference)/create-reference",
        "/(team)/create-team",
        "/(reference)/edit-reference/$refId"
      ]
    },
    "/": {
      "filePath": "index.lazy.tsx"
    },
    "/document": {
      "filePath": "document.tsx",
      "children": [
        "/document/$fileName"
      ]
    },
    "/library": {
      "filePath": "library.tsx"
    },
    "/contact": {
      "filePath": "contact.lazy.tsx"
    },
    "/(auth)": {
      "filePath": "(auth)",
      "children": [
        "/(auth)/_auth",
        "/(auth)/login",
        "/(auth)/logout",
        "/(auth)/reset-password",
        "/(auth)/signup",
        "/(auth)/usersettings"
      ]
    },
    "/(auth)/_auth": {
      "filePath": "(auth)/_auth.tsx",
      "parent": "/(auth)",
      "children": [
        "/(auth)/_auth/admin-dashboard"
      ]
    },
    "/document/$fileName": {
      "filePath": "document.$fileName.tsx",
      "parent": "/document"
    },
    "/(auth)/login": {
      "filePath": "(auth)/login.lazy.tsx",
      "parent": "/(auth)"
    },
    "/(auth)/logout": {
      "filePath": "(auth)/logout.lazy.tsx",
      "parent": "/(auth)"
    },
    "/(auth)/reset-password": {
      "filePath": "(auth)/reset-password.lazy.tsx",
      "parent": "/(auth)"
    },
    "/(auth)/signup": {
      "filePath": "(auth)/signup.lazy.tsx",
      "parent": "/(auth)"
    },
    "/(auth)/usersettings": {
      "filePath": "(auth)/usersettings.lazy.tsx",
      "parent": "/(auth)"
    },
    "/(reference)/create-reference": {
      "filePath": "(reference)/create-reference.lazy.tsx"
    },
    "/(team)/create-team": {
      "filePath": "(team)/create-team.lazy.tsx"
    },
    "/(auth)/_auth/admin-dashboard": {
      "filePath": "(auth)/_auth.admin-dashboard.tsx",
      "parent": "/(auth)/_auth"
    },
    "/(reference)/edit-reference/$refId": {
      "filePath": "(reference)/edit-reference.$refId.tsx"
    }
  }
}
ROUTE_MANIFEST_END */
