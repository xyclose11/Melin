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
                                Track Academic References
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Melin is designed to make the process of
                                creating references as easy and fast as
                                possible.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Import/Export
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                The process of finding the perfect reference
                                management system is time-consuming, with that
                                in mind Melin is able to import and export in 3
                                formats: CSV, CSL-JSON, and BibLaTex
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <Badge
                            variant="secondary"
                            className="w-full justify-center text-center"
                        >
                            Coming Soon
                        </Badge>
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Collaborate with colleagues
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Invite team-members to edit documents, reference
                                lists, and more!
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <Badge
                            variant="secondary"
                            className="w-full justify-center text-center"
                        >
                            Coming Soon
                        </Badge>
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                10,000+ Citation Styles
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Melin has the ability to generate citations and
                                bibliographies based on the references provided
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <Badge
                            variant="secondary"
                            className="w-full justify-center text-center"
                        >
                            Coming Soon
                        </Badge>
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">Search</h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Melin utilizes several API providers to provide
                                responsive yet integral data, based on multiple
                                search parameters.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Open Source
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Melin is 100% open source and can be found on
                                GitHub
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md aspect-square p-6 flex justify-between flex-col">
                        <BookOpenText size={28} strokeWidth={1} />
                        <Badge
                            variant="secondary"
                            className="w-full justify-center text-center"
                        >
                            Coming Soon
                        </Badge>
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                QR Scanning
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Scan any QR code that resembles an ISBN, ISSN,
                                or DOI and Melin will import the applicable
                                information.
                            </p>
                        </div>
                    </div>

                    <div className="bg-muted h-full rounded-md p-6 flex justify-between flex-col lg:col-span-2">
                        <BookOpenText size={28} strokeWidth={1} />
                        <Badge
                            variant="secondary"
                            className="w-full justify-center text-center"
                        >
                            Coming Soon
                        </Badge>
                        <div className="flex flex-col">
                            <h3 className="text-xl tracking-tight">
                                Custom Fields
                            </h3>
                            <p className="text-muted-foreground max-w-xs text-base">
                                Don't like the structured format of the Book
                                type? With Melin you can add your own fields to
                                better fit your References.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
);
