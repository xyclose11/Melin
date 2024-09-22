"use client";

import { useEffect, useState } from "react";
import { columns } from "./LibraryComponents/columns";
import { DataTable } from "@/routes/LibraryComponents/library-table";
import { UserNav } from "@/routes/LibraryComponents/user-nav";
import { z } from "zod";
import { Task, taskSchema } from "@/routes/LibraryComponents/data/schema";


export default function LibraryPage() {
    const [tasks, setTasks] = useState<Task[]>([]); // Initialize with empty array

    useEffect(() => {
        getTasks();
    }, []);

    const getTasks = async () => {
        try {
            const response = await fetch('/Reference');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const parsedTasks = await response.json();
            setTasks(z.array(taskSchema).parse(parsedTasks));
        } catch (err) {
            console.error("Error fetching tasks:", err);
        }
    };


    return (
        <div className="hidden h-full flex-1 flex-col space-y-8 p-8 md:flex">
            <div className="flex items-center justify-between space-y-2">
                <div>
                    <h2 className="text-2xl font-bold tracking-tight">Welcome back!</h2>
                    <p className="text-muted-foreground">
                        Here&apos;s a list of your tasks for this month!
                    </p>
                </div>
                <div className="flex items-center space-x-2">
                    <UserNav />
                </div>
            </div>
            <DataTable data={tasks} columns={columns} />
        </div>
    );
}
