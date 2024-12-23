import { BookOpenText, User } from "lucide-react";
import { Badge } from "@/components/ui/badge";

export const Features = () => (
    <div className="w-full py-20 lg:py-40">
        <div className="container mx-auto">
            <div className="flex flex-col gap-10">
                <div className="flex gap-4 flex-col items-start">
                    <div>
                        <Badge>Features</Badge>
                    </div>
                    <div className="flex gap-2 flex-col">
                        <h2 className="text-3xl md:text-5xl tracking-tighter max-w-xl font-regular text-left">
                            What Melin Offers
                        </h2>
                        <p className="text-lg max-w-xl lg:max-w-lg leading-relaxed tracking-tight text-muted-foreground  text-left">
                            Writing a paper is already hard enough.
                        </p>
                    </div>
                </div>
                <div className="flex flex-col sm:grid sm:grid-cols-2 lg:grid  lg:grid-cols-3 xl:grid-cols-4 gap-8">
                    <div className="bg-muted h-full w-full rounded-md aspect-square p-6 flex justify-between flex-col lg:col-span-2 lg:row-span-2">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Pay supplier invoices
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Our goal is to streamline SMB trade, making it
                                easier and faster than ever.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Pay supplier invoices
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Our goal is to streamline SMB trade, making it
                                easier and faster than ever.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Pay supplier invoices
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Our goal is to streamline SMB trade, making it
                                easier and faster than ever.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Pay supplier invoices
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Our goal is to streamline SMB trade, making it
                                easier and faster than ever.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Pay supplier invoices
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Our goal is to streamline SMB trade, making it
                                easier and faster than ever.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Pay supplier invoices
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Our goal is to streamline SMB trade, making it
                                easier and faster than ever.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Pay supplier invoices
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Our goal is to streamline SMB trade, making it
                                easier and faster than ever.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md p-6 flex justify-between flex-col lg:col-span-2">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Pay supplier invoices
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Our goal is to streamline SMB trade, making it
                                easier and faster than ever.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
);
