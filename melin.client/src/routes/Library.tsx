"use client";

import * as React from "react";
// import { DndContext } from "@dnd-kit/core";
// import { useState } from "react";
// import { LibrarySideBar } from "@/routes/LibraryViews/LibrarySideBar.tsx";
// import { DroppableWorkspace } from "@/routes/LibraryViews/DragNDrop/DroppableWorkspace.tsx";

import { ToastAction } from "@/components/ui/toast";
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
import { Input } from "@/components/ui/input";
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import { ReactNode, useEffect, useState } from "react";
import { instance } from "@/utils/axiosInstance.ts";
import { useToast } from "@/hooks/use-toast.ts";
import { LibrarySideBar } from "@/routes/LibraryViews/LibrarySideBar.tsx";
import { DraggableGroup } from "@/routes/GroupComponents/DraggableGroup.tsx";
import { TagTableDisplay } from "@/routes/TagComponents/TagTableDisplay.tsx";

export enum CREATOR_TYPES {
    Author = "Author",
}

type GroupType = {
    name: string;
    nodes: [];
};

type Creator = {
    id: number;
    type: CREATOR_TYPES;
    firstName: string;
    lastName: string;
};

type ReferenceTag = {
    id: number;
    text: string;
    createdBy: string;
};

export type Reference = {
    id: number;
    type: string;
    title: string;
    creators: Creator[];
    language: string;
};

export function LibraryPage() {
    const [sorting, setSorting] = React.useState<SortingState>([]);
    const [columnFilters, setColumnFilters] =
        React.useState<ColumnFiltersState>([]);
    const [columnVisibility, setColumnVisibility] =
        React.useState<VisibilityState>({});
    const [rowSelection, setRowSelection] = React.useState({});
    const [totalRef, setTotalRef] = useState(0);
    const [data, setData] = React.useState<Reference[]>([]);
    const { toast } = useToast();
    // const [isDropped, setIsDropped] = useState(false);
    // const libSideBar = <LibrarySideBar>Drag me</LibrarySideBar>;

    const [pagination, setPagination] = useState({
        pageSize: 10,
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
            accessorKey: "title",
            header: "Title",
            cell: ({ row }) => (
                <div className="capitalize">{row.getValue("title")}</div>
            ),
        },
        {
            accessorKey: "type",
            header: "Type",
            cell: ({ row }) => (
                <div className="capitalize">{row.getValue("type")}</div>
            ),
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

            cell: ({ row }) => {
                const creators: Creator[] = row.getValue("creators");
                return (
                    <div>
                        {creators.map((creator) => (
                            <div key={creator.id}>
                                <div>{creator.type}</div>
                                <div>{creator.firstName}</div>
                                <div>{creator.lastName}</div>
                            </div>
                        ))}
                    </div>
                );
            },
        },
        {
            accessorKey: "tags",
            header: "Tags",
            enableHiding: true,
            cell: ({ row }) => {
                const tags: ReferenceTag[] = row.getValue("tags");
                return (
                    <div>
                        {tags.map((tag) => (
                            <TagTableDisplay
                                key={tag.id}
                                tagId={tag.id}
                                name={tag.text}
                            />
                        ))}
                    </div>
                );
            },
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
    const fetchData = async () => {
        try {
            const response = await instance.get(
                `Reference/references?pageNumber=${pagination.pageIndex}&pageSize=${pagination.pageSize}`,
                {
                    withCredentials: true,
                },
            );
            setData(response.data.data);
            setTotalRef(response.data.TotalPages);
        } catch (error) {
            toast({
                variant: "destructive",
                title: "Unable to get References",
                description: "Please try again later",
                action: (
                    <ToastAction altText={"Try Again"}>Try Again</ToastAction>
                ),
            });
            console.error("Unable to get references:", error);
        }
    };

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

            console.log(response);
        } catch (error) {
            console.error("Unable to delete reference:", error);
        }
    };

    useEffect(() => {
        fetchData();
    }, [pagination]);

    const table = useReactTable({
        data,
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
        onPaginationChange: setPagination,
        rowCount: totalRef,
        state: {
            sorting,
            columnFilters,
            columnVisibility,
            rowSelection,
            pagination,
        },
    });

    const [userGroups, setUserGroups] = useState<ReactNode[]>([]);

    const getGroups = async () => {
        try {
            const res = await instance.get("get-owned-groups", {
                withCredentials: true,
            });

            if (res.status === 200) {
                console.log(res);
                // see if user has groups
                if (res.data.length <= 0) {
                    console.log("NO GROUPS");
                }

                setUserGroups(res.data);
            } else {
                // DISPLAY ERROR
            }
        } catch (e) {
            console.log(e);
            // DISPLAY ERROR
        }
    };

    useEffect(() => {
        getGroups();
    }, []);

    return (
        <div className={"flex gap-3"}>
            {/*<DndContext onDragEnd={handleDragEnd}>*/}
            {/*    {!isDropped ? libSideBar : null}*/}
            {/*    <DroppableWorkspace>*/}
            {/*        {isDropped ? libSideBar : "Drop Here"}*/}
            {/*    </DroppableWorkspace>*/}
            {/*</DndContext>*/}
            <LibrarySideBar>

                {userGroups.map((g: GroupType) => (
                    <DraggableGroup key={g.name} groupName={g.name} groupNodes={g.references} />
                ))}
            </LibrarySideBar>{" "}
            <div className="w-full light">
                <div className="flex items-center py-4">
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
                            onClick={() => table.previousPage()}
                            disabled={!table.getCanPreviousPage()}
                        >
                            Previous
                        </Button>
                        <Button
                            variant="outline"
                            size="sm"
                            onClick={() => {
                                setPagination((prev) => ({
                                    ...prev,
                                    pageIndex: prev.pageIndex + 1, // Increment pageIndex here
                                }));
                            }}
                        >
                            Next
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    );

    // function handleDragEnd(event: any) {
    //     if (event.over && event.over.id === "droppable") {
    //         setIsDropped(true);
    //     }
    // }
}
