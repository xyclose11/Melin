"use client";

import * as React from "react";
import { useState } from "react";

import { ToastAction } from "@/components/ui/toast";
import {
    ColumnDef,
    ColumnFiltersState,
    flexRender,
    getCoreRowModel,
    getFilteredRowModel,
    getPaginationRowModel,
    getSortedRowModel,
    SortingState,
    useReactTable,
    VisibilityState,
} from "@tanstack/react-table";
import {
    ArrowUpDown,
    ChevronDown,
    MoreHorizontal,
    SquarePlusIcon,
} from "lucide-react";

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
import { Input } from "@/components/ui/input";
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import { instance } from "@/utils/axiosInstance.ts";
import { useToast } from "@/hooks/use-toast.ts";
import { TagTableDisplay } from "@/TagComponents/TagTableDisplay.tsx";
import { useReferenceSelection } from "@/Context/ReferencesSelectedContext.tsx";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
} from "@/components/ui/dialog";
import { AddTagToReference } from "@/Tag/AddTagToReference.tsx";
import { Link } from "@tanstack/react-router";
import { Pagination } from "@/api/referencesQueryOptions.tsx";
import { useQuery } from "@tanstack/react-query";
import { fetchReferences } from "@/api/fetchReferences.ts";
import { Card, CardContent } from "@/components/ui/card.tsx";

export enum CREATOR_TYPES {
    Author = "Author",
}

type Creator = {
    id: number;
    types: CREATOR_TYPES;
    firstName: string;
    lastName: string;
};

export type ReferenceTag = {
    id: number | string;
    text: string;
    createdBy: string;
};

export type Reference = {
    id: number;
    type: string;
    title: string;
    datePublished: string;
    updatedAt: string;
    createdAt: string;
    creators: Creator[];
    language: string;
};

export function Library({ initialData }: { initialData: Reference[] }) {
    const [sorting, setSorting] = React.useState<SortingState>([]);
    const [columnFilters, setColumnFilters] =
        React.useState<ColumnFiltersState>([]);
    const [columnVisibility, setColumnVisibility] =
        React.useState<VisibilityState>({
            createdAt: false,
            updatedAt: false,
        });
    const [rowSelection, setRowSelection] = React.useState({});
    const { toast } = useToast();

    const { selectedReferences, toggleReference, clearSelection } =
        useReferenceSelection();

    // const { selectedGroup } = useGroupSelection();
    const [data, setData] = React.useState<Reference[]>(initialData);
    const [pagination] = useState<Pagination>({
        pageSize: 15,
        pageIndex: 0,
    });

    const columns: ColumnDef<Reference>[] = [
        {
            id: "select",
            header: ({ table }) => (
                <Checkbox
                    checked={
                        table.getIsAllPageRowsSelected() ||
                        (table.getIsSomePageRowsSelected() && "indeterminate")
                    }
                    onCheckedChange={(value) => {
                        if (value) {
                            table
                                .getRowModel()
                                .rows.forEach((row) =>
                                    toggleReference(row.original.id),
                                );
                        } else {
                            clearSelection();
                        }
                    }}
                    aria-label="Select all"
                />
            ),
            cell: ({ row }) => (
                <Checkbox
                    checked={selectedReferences.includes(row.original.id)}
                    onCheckedChange={() => toggleReference(row.original.id)}
                    aria-label="Select row"
                />
            ),
            enableSorting: false,
            enableHiding: false,
            size: 250,
            enableResizing: true,
        },
        {
            accessorKey: "title",
            header: ({ column }) => {
                return (
                    <div className={"flex"}>
                        Title
                        <ArrowUpDown
                            className="ml-2 h-4 w-4"
                            onClick={() =>
                                column.toggleSorting(
                                    column.getIsSorted() === "asc",
                                )
                            }
                        />
                    </div>
                );
            },
            cell: ({ row }) => (
                <Link
                    className="capitalize text-xs"
                    to={`/edit-reference/${row.original.id}`}
                >
                    {row.getValue("title")}
                </Link>
            ),
            enableSorting: true,
            enableHiding: true,
        },

        {
            accessorKey: "creators",
            header: ({ column }) => {
                return (
                    <Button
                        variant="ghost"
                        onClick={() =>
                            column.toggleSorting(column.getIsSorted() === "asc")
                        }
                    >
                        Creators
                        <ArrowUpDown className="ml-2 h-4 w-4" />
                    </Button>
                );
            },
            enablePinning: true,
            minSize: 0,
            maxSize: 3,
            size: 1,

            cell: ({ row }) => {
                const creators: Creator[] = row.getValue("creators");
                if (creators.length <= 0) {
                    return (
                        <Card>
                            <CardContent></CardContent>
                        </Card>
                    );
                }

                return (
                    <Card className="h-20 pt-2">
                        <CardContent className="">
                            <div className="">
                                {creators
                                    .slice(0, 4)
                                    .map((creator: Creator) => (
                                        <div
                                            className="text-xs grid grid-cols-2 sm:text-[10px] md:text-xs"
                                            key={creator.id}
                                        >
                                            <div>{creator.firstName}</div>
                                            <div>{creator.lastName}</div>
                                        </div>
                                    ))}
                            </div>
                        </CardContent>
                    </Card>
                );
            },
        },
        {
            accessorKey: "tags",
            header: "Tags",
            enableHiding: true,
            cell: ({ row }) => {
                const [tags, setTags] = useState<ReferenceTag[]>(
                    row.getValue("tags"),
                );

                const handleRemoveTag = (tagToRemoveId: any) => {
                    const filteredTags = tags.filter((t) => {
                        return t.id !== tagToRemoveId;
                    });

                    setTags(filteredTags);
                };

                return (
                    <div className={"max-w-[25%]"}>
                        <div className={"flex gap-1 flex-wrap"}>
                            {tags === undefined ? (
                                <div></div>
                            ) : (
                                tags
                                    .slice(0, 5)
                                    .map((tag) => (
                                        <TagTableDisplay
                                            key={tag.id}
                                            tagId={
                                                typeof tag.id === "number"
                                                    ? tag.id
                                                    : -1
                                            }
                                            refId={row.original.id}
                                            name={tag.text}
                                            handleRemoveTag={handleRemoveTag}
                                        />
                                    ))
                            )}
                            <div className={"justify-self-end self-end"}>
                                <Dialog>
                                    <DialogTrigger>
                                        <SquarePlusIcon />
                                    </DialogTrigger>
                                    <DialogContent>
                                        <DialogHeader>
                                            <DialogTitle>
                                                Add Tag(s)
                                            </DialogTitle>
                                        </DialogHeader>
                                        <AddTagToReference
                                            refId={row.original.id}
                                            stateChanger={setTags}
                                        />
                                    </DialogContent>
                                </Dialog>
                            </div>
                        </div>
                    </div>
                );
            },
        },
        {
            accessorKey: "locationStored",
            header: ({ column }) => {
                return (
                    <div className={"flex"}>
                        Location Stored
                        <ArrowUpDown
                            className="ml-2 h-4 w-4"
                            onClick={() =>
                                column.toggleSorting(
                                    column.getIsSorted() === "asc",
                                )
                            }
                        />
                    </div>
                );
            },
            cell: ({ row }) => (
                <div className="capitalize">
                    {row.getValue("locationStored")}
                </div>
            ),
            enableSorting: true,
        },
        {
            accessorKey: "datePublished",
            header: ({ column }) => {
                return (
                    <div className={"flex"}>
                        Date Published
                        <ArrowUpDown
                            className="ml-2 h-4 w-4"
                            onClick={() =>
                                column.toggleSorting(
                                    column.getIsSorted() === "asc",
                                )
                            }
                        />
                    </div>
                );
            },
            cell: ({ row }) => (
                <div className="capitalize">
                    {row.getValue("datePublished")}
                </div>
            ),
            enableSorting: true,
        },
        {
            accessorKey: "updatedAt",
            header: ({ column }) => {
                return (
                    <div className={"flex"}>
                        Last Updated At
                        <ArrowUpDown
                            className="ml-2 h-4 w-4"
                            onClick={() =>
                                column.toggleSorting(
                                    column.getIsSorted() === "asc",
                                )
                            }
                        />
                    </div>
                );
            },
            cell: ({ row }) => (
                <div className="capitalize">
                    {formatRowDate(row.getValue("updatedAt"))}
                </div>
            ),
            enableSorting: true,
        },
        {
            accessorKey: "createdAt",
            header: ({ column }) => {
                return (
                    <div className={"flex"}>
                        Created On
                        <ArrowUpDown
                            className="ml-2 h-4 w-4"
                            onClick={() =>
                                column.toggleSorting(
                                    column.getIsSorted() === "asc",
                                )
                            }
                        />
                    </div>
                );
            },
            cell: ({ row }) => (
                <div className="capitalize">
                    {formatRowDate(row.getValue(`createdAt`))}
                </div>
            ),
            enableSorting: true,
        },
        {
            id: "actions",
            enableHiding: false,
            cell: ({ row }) => {
                const reference = row.original;

                return (
                    <DropdownMenu>
                        <DropdownMenuTrigger asChild>
                            <Button variant="ghost" className="h-8 w-8 p-0">
                                <span className="sr-only">Open menu</span>
                                <MoreHorizontal className="h-4 w-4" />
                            </Button>
                        </DropdownMenuTrigger>
                        <DropdownMenuContent align="end">
                            <DropdownMenuLabel>Actions</DropdownMenuLabel>
                            <DropdownMenuItem
                                onClick={() =>
                                    navigator.clipboard.writeText(
                                        reference.title,
                                    )
                                }
                            >
                                Copy title
                            </DropdownMenuItem>
                            <DropdownMenuSeparator />
                            <DropdownMenuItem>Add to Group</DropdownMenuItem>

                            <DropdownMenuSeparator />
                            <DropdownMenuItem>Edit</DropdownMenuItem>
                            <DropdownMenuItem
                                onClick={() =>
                                    handleReferenceDelete(reference.id)
                                }
                            >
                                Delete
                            </DropdownMenuItem>
                        </DropdownMenuContent>
                    </DropdownMenu>
                );
            },
        },
    ];

    function formatRowDate(val: string): string {
        const date: Date = new Date(val);
        return date.toUTCString();
    }

    const handleReferenceDelete = async (referenceId: number) => {
        try {
            const response = await instance.delete(
                `Reference/delete-reference?refId=${referenceId}`,
                {
                    withCredentials: true,
                },
            );

            // Check if the delete operation was successful
            if (response.status === 200) {
                // Update state to remove the deleted reference
                setData((prevData) =>
                    prevData.filter((ref) => ref.id !== referenceId),
                );

                // show success alert & offer undo

                toast({
                    variant: "destructive",
                    title: "Reference Successfully Deleted",
                    description: `Reference with ID: ${referenceId} has been deleted.`,
                    action: <ToastAction altText={"Undo"}>Undo</ToastAction>,
                });
            }
        } catch (error) {
            console.error("Unable to delete reference:", error);
        }
    };

    const { data: queryData } = useQuery({
        queryKey: ["references", pagination.pageIndex, pagination.pageSize],
        queryFn: () => fetchReferences(pagination, null),
    });

    const table = useReactTable({
        data: queryData?.data.data ?? data,
        columns,
        onSortingChange: setSorting,
        onColumnFiltersChange: setColumnFilters,
        getCoreRowModel: getCoreRowModel(),
        getPaginationRowModel: getPaginationRowModel(),
        getSortedRowModel: getSortedRowModel(),
        getFilteredRowModel: getFilteredRowModel(),
        onColumnVisibilityChange: setColumnVisibility,
        onRowSelectionChange: setRowSelection,
        manualPagination: true,
        columnResizeMode: "onEnd",
        columnResizeDirection: "rtl",
        rowCount: queryData?.data.totalRecords ?? data.length,
        state: {
            sorting,
            columnFilters,
            columnVisibility,
            rowSelection,
        },
    });

    return (
        <div className={"flex gap-0"}>
            <div className="w-full light">
                <div className="flex items-center py-4 mr-1 ml-1">
                    <Input
                        placeholder="Filter references..."
                        value={
                            (table
                                .getColumn("title")
                                ?.getFilterValue() as string) ?? ""
                        }
                        onChange={(event) =>
                            table
                                .getColumn("title")
                                ?.setFilterValue(event.target.value)
                        }
                        className="max-w-sm"
                    />
                    <DropdownMenu>
                        <DropdownMenuTrigger asChild>
                            <Button variant="outline" className="ml-auto">
                                Columns <ChevronDown className="ml-2 h-4 w-4" />
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
                                                column.toggleVisibility(value)
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
                    <Table
                        className={
                            "w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400"
                        }
                    >
                        <TableHeader>
                            {table.getHeaderGroups().map((headerGroup) => (
                                <TableRow key={headerGroup.id}>
                                    {headerGroup.headers.map((header) => {
                                        return (
                                            <TableHead key={header.id}>
                                                {header.isPlaceholder
                                                    ? null
                                                    : flexRender(
                                                          header.column
                                                              .columnDef.header,
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
                        {table.getFilteredRowModel().rows.length} row(s)
                        selected.
                    </div>
                    <div className="space-x-2">
                        <Button
                            variant="outline"
                            size="sm"
                            onClick={() => {
                                // setPagination((prev) => ({
                                //     ...prev,
                                //     pageIndex: prev.pageIndex - 1,
                                // }));
                                table.previousPage();
                            }}
                            disabled={
                                !table.getCanPreviousPage() ||
                                pagination.pageIndex === 0
                            }
                        >
                            Previous
                        </Button>
                        <Button
                            variant="outline"
                            size="sm"
                            onClick={() => {
                                // setPagination((prev) => ({
                                //     ...prev,
                                //     pageIndex: prev.pageIndex + 1,
                                // }));
                                table.nextPage();
                            }}
                            disabled={!table.getCanNextPage()}
                        >
                            Next
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    );
}
