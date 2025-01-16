"use client";

import * as React from "react";
import {
    ColumnDef,
    ColumnFiltersState,
    SortingState,
    VisibilityState,
    flexRender,
    getCoreRowModel,
    getFilteredRowModel,
    getPaginationRowModel,
    getSortedRowModel,
    useReactTable,
} from "@tanstack/react-table";
import { ArrowUpDown, ChevronDown, MoreHorizontal } from "lucide-react";

import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import {
    DropdownMenu,
    DropdownMenuCheckboxItem,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";

import { Input } from "@/components/ui/input";
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import { Roles } from "@/utils/AuthProvider.tsx";
import { useQuery } from "@tanstack/react-query";
import fetchAllUsers from "@/api/fetchAllUsers.ts";

export type UserTableView = {
    id: string;
    fullName: number;
    email: string;
    lastLoginDate: Date;
    accessFailedCount: number;
    phoneNumberConfirmed: boolean;
    emailConfirmed: boolean;
    lockoutEnabled: boolean;
    userName: string;
    twoFactorEnabled: boolean;
    referenceCount: number;
    roles: Roles;
    status: "inactive" | "active" | "archived";
};

export const columns: ColumnDef<UserTableView>[] = [
    {
        id: "select",
        header: ({ table }) => (
            <Checkbox
                checked={
                    table.getIsAllPageRowsSelected() ||
                    (table.getIsSomePageRowsSelected() && "indeterminate")
                }
                onCheckedChange={(value) =>
                    table.toggleAllPageRowsSelected(!!value)
                }
                aria-label="Select all"
            />
        ),
        cell: ({ row }) => (
            <Checkbox
                checked={row.getIsSelected()}
                onCheckedChange={(value) => row.toggleSelected(!!value)}
                aria-label="Select row"
            />
        ),
        enableSorting: false,
        enableHiding: false,
    },
    {
        accessorKey: "id",
        header: "ID",
        cell: ({ row }) => (
            <div className="capitalize text-xs">{row.getValue("id")}</div>
        ),
    },
    {
        accessorKey: "email",
        header: ({ column }) => {
            return (
                <Button
                    variant="ghost"
                    onClick={() =>
                        column.toggleSorting(column.getIsSorted() === "asc")
                    }
                >
                    Email
                    <ArrowUpDown />
                </Button>
            );
        },
        cell: ({ row }) => (
            <div className="lowercase">{row.getValue("email")}</div>
        ),
    },
    {
        accessorKey: "roles",
        header: () => <div>Roles</div>,
        cell: ({ row }) => {
            return (
                <Select>
                    <SelectTrigger className="w-[180px]">
                        <SelectValue placeholder={row.getValue("roles")} />
                    </SelectTrigger>
                    <SelectContent>
                        {(Object.keys(Roles) as Array<keyof typeof Roles>).map(
                            (key) => {
                                return (
                                    <SelectItem key={key} value={key}>
                                        {key}
                                    </SelectItem>
                                );
                            },
                        )}
                    </SelectContent>
                </Select>
            );
        },
    },
    {
        accessorKey: "phoneNumberConfirmed",
        header: () => <div>Phone Number Confirmed?</div>,
        cell: ({ row }) => {
            return (
                <div>
                    {row.getValue("phoneNumberConfirmed") ? "true" : "false"}
                </div>
            );
        },
    },
    {
        accessorKey: "emailConfirmed",
        header: () => <div>Email Confirmed?</div>,
        cell: ({ row }) => {
            return (
                <div>{row.getValue("emailConfirmed") ? "true" : "false"}</div>
            );
        },
    },
    {
        accessorKey: "lockoutEnabled",
        header: () => <div>Lockout Enabled</div>,
        cell: ({ row }) => {
            return (
                <div>{row.getValue("lockoutEnabled") ? "true" : "false"}</div>
            );
        },
    },
    {
        accessorKey: "twoFactorEnabled",
        header: () => <div>2FA</div>,
        cell: ({ row }) => {
            return (
                <div>{row.getValue("twoFactorEnabled") ? "true" : "false"}</div>
            );
        },
    },
    {
        accessorKey: "referenceCount", // TODO: Implement accordion that will display the users references
        header: () => <div>Reference Count</div>,
        cell: ({ row }) => {
            return <div>{row.getValue("referenceCount")}</div>;
        },
    },
    {
        accessorKey: "fullName",
        header: () => <div>Name *Not Currently Implemented*</div>,
        cell: ({ row }) => {
            return <div>{row.getValue("fullName")}</div>;
        },
    },
    {
        accessorKey: "accessFailedCount",
        header: () => <div className="text-right">Failed Access Count</div>,
        cell: ({ row }) => {
            return (
                <div className="text-right font-medium">
                    {row.getValue("accessFailedCount")}
                </div>
            );
        },
    },
    {
        id: "actions",
        enableHiding: false,
        cell: ({ row }) => {
            const payment = row.original;

            return (
                <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                        <Button variant="ghost" className="h-8 w-8 p-0">
                            <span className="sr-only">Open menu</span>
                            <MoreHorizontal />
                        </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                        <DropdownMenuLabel>Actions</DropdownMenuLabel>
                        <DropdownMenuItem
                            onClick={() =>
                                navigator.clipboard.writeText(payment.id)
                            }
                        >
                            Copy User ID
                        </DropdownMenuItem>
                        <DropdownMenuSeparator />
                        <DropdownMenuItem>View User Details</DropdownMenuItem>
                        <DropdownMenuItem>
                            Remove User Auth Session
                        </DropdownMenuItem>
                        <DropdownMenuItem>Reset User Password</DropdownMenuItem>
                        <DropdownMenuItem>Delete User</DropdownMenuItem>
                    </DropdownMenuContent>
                </DropdownMenu>
            );
        },
    },
];

export default function AdminUserTable() {
    const [sorting, setSorting] = React.useState<SortingState>([]);
    const [columnFilters, setColumnFilters] =
        React.useState<ColumnFiltersState>([]);
    const [columnVisibility, setColumnVisibility] =
        React.useState<VisibilityState>({});
    const [rowSelection, setRowSelection] = React.useState({});

    const {
        data: queryData,
        isError,
        error,
    } = useQuery({
        queryKey: ["users"],
        queryFn: fetchAllUsers,
    });

    if (isError) {
        return <div>{error.message}</div>;
    }

    const table = useReactTable({
        data: queryData?.data ?? "",
        columns,
        onSortingChange: setSorting,
        onColumnFiltersChange: setColumnFilters,
        getCoreRowModel: getCoreRowModel(),
        getPaginationRowModel: getPaginationRowModel(),
        getSortedRowModel: getSortedRowModel(),
        getFilteredRowModel: getFilteredRowModel(),
        onColumnVisibilityChange: setColumnVisibility,
        onRowSelectionChange: setRowSelection,
        state: {
            sorting,
            columnFilters,
            columnVisibility,
            rowSelection,
        },
    });

    return (
        <div className="w-full h-full">
            <div className="flex items-center py-4">
                <Input
                    placeholder="Filter emails..."
                    value={
                        (table
                            .getColumn("email")
                            ?.getFilterValue() as string) ?? ""
                    }
                    onChange={(event) =>
                        table
                            .getColumn("email")
                            ?.setFilterValue(event.target.value)
                    }
                    className="max-w-sm"
                />
                <Button
                    className="ml-4 mr-4"
                    type="reset"
                    variant="destructive"
                    disabled={
                        !table.getIsAllPageRowsSelected() &&
                        !table.getIsSomePageRowsSelected()
                    }
                >
                    Cancel
                </Button>
                <Button
                    type="submit"
                    variant="secondary"
                    disabled={
                        !table.getIsAllPageRowsSelected() &&
                        !table.getIsSomePageRowsSelected()
                    }
                >
                    Save Changes
                </Button>
                <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                        <Button variant="outline" className="ml-auto">
                            Columns <ChevronDown />
                        </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                        {table
                            .getAllColumns()
                            .filter((column) => column.getCanHide())
                            .map((column) => {
                                return (
                                    <DropdownMenuCheckboxItem
                                        key={column.id}
                                        className="capitalize"
                                        checked={column.getIsVisible()}
                                        onCheckedChange={(value) =>
                                            column.toggleVisibility(!!value)
                                        }
                                    >
                                        {column.id}
                                    </DropdownMenuCheckboxItem>
                                );
                            })}
                    </DropdownMenuContent>
                </DropdownMenu>
            </div>
            <div className="rounded-md border">
                <Table>
                    <TableHeader>
                        {table.getHeaderGroups().map((headerGroup) => (
                            <TableRow key={headerGroup.id}>
                                {headerGroup.headers.map((header) => {
                                    return (
                                        <TableHead key={header.id}>
                                            {header.isPlaceholder
                                                ? null
                                                : flexRender(
                                                      header.column.columnDef
                                                          .header,
                                                      header.getContext(),
                                                  )}
                                        </TableHead>
                                    );
                                })}
                            </TableRow>
                        ))}
                    </TableHeader>
                    <TableBody>
                        {table.getRowModel().rows?.length ? (
                            table.getRowModel().rows.map((row) => (
                                <TableRow
                                    key={row.id}
                                    data-state={
                                        row.getIsSelected() && "selected"
                                    }
                                >
                                    {row.getVisibleCells().map((cell) => (
                                        <TableCell key={cell.id}>
                                            {flexRender(
                                                cell.column.columnDef.cell,
                                                cell.getContext(),
                                            )}
                                        </TableCell>
                                    ))}
                                </TableRow>
                            ))
                        ) : (
                            <TableRow>
                                <TableCell
                                    colSpan={columns.length}
                                    className="h-24 text-center"
                                >
                                    No results.
                                </TableCell>
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </div>
            <div className="flex items-center justify-end space-x-2 py-4">
                <div className="flex-1 text-sm text-muted-foreground">
                    {table.getFilteredSelectedRowModel().rows.length} of{" "}
                    {table.getFilteredRowModel().rows.length} row(s) selected.
                </div>
                <div className="space-x-2">
                    <Button
                        variant="outline"
                        size="sm"
                        onClick={() => table.previousPage()}
                        disabled={!table.getCanPreviousPage()}
                    >
                        Previous
                    </Button>
                    <Button
                        variant="outline"
                        size="sm"
                        onClick={() => table.nextPage()}
                        disabled={!table.getCanNextPage()}
                    >
                        Next
                    </Button>
                </div>
            </div>
        </div>
    );
}
