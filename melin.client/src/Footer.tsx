import { Link } from "@tanstack/react-router";

export const Footer = () => {
    const navigationItems = [
        {
            title: "Home",
            href: "/",
            description: "",
        },
        {
            title: "Your References",
            description: "Writing a paper is already tough.",
            items: [
                {
                    title: "Library",
                    href: "/library",
                },
                {
                    title: "Statistics",
                    href: "/statistics",
                },
                {
                    title: "Import",
                    href: "/import",
                },
                {
                    title: "Export",
                    href: "/export",
                },
                {
                    title: "Search",
                    href: "/search",
                },
            ],
        },
        {
            title: "Melin",
            description: "Writing a paper is already tough.",
            items: [
                {
                    title: "About",
                    href: "/about",
                },
                {
                    title: "Documentation",
                    href: "/documentation",
                },
                {
                    title: "Feedback",
                    href: "/feedback",
                },
                {
                    title: "Contact",
                    href: "/contact",
                },
            ],
        },
    ];

    return (
        <div className="bottom-0 w-full py-10 lg:py-20 bg-foreground text-background">
            <div className="container mx-auto">
                <div className="grid lg:grid-cols-2 gap-10 items-center">
                    <div className="flex gap-8 flex-col items-start">
                        <div className="flex gap-2 flex-col">
                            <h2 className="text-3xl md:text-5xl tracking-tighter max-w-xl font-regular text-left">
                                Melin
                            </h2>
                            <p className="text-lg max-w-lg leading-relaxed tracking-tight text-background/75 text-left">
                                Writing a paper is already tough.
                            </p>
                        </div>
                        <div className="flex gap-20 flex-row">
                            <div className="flex flex-col text-sm max-w-lg leading-relaxed tracking-tight text-background/75 text-left">
                                <p>Valparaiso University</p>
                            </div>
                            <div className="flex flex-col text-sm max-w-lg leading-relaxed tracking-tight text-background/75 text-left">
                                <Link href="/">Terms of service</Link>
                                <Link href="/">Privacy Policy</Link>
                            </div>
                        </div>
                    </div>
                    <div className="grid lg:grid-cols-3 gap-10 items-start">
                        {navigationItems.map((item) => (
                            <div
                                key={item.title}
                                className="flex text-base gap-1 flex-col items-start"
                            >
                                <div className="flex flex-col gap-2">
                                    {item.href ? (
                                        <Link
                                            href={item.href}
                                            className="flex justify-between items-center"
                                        >
                                            <span className="text-xl">
                                                {item.title}
                                            </span>
                                        </Link>
                                    ) : (
                                        <p className="text-xl">{item.title}</p>
                                    )}
                                    {item.items &&
                                        item.items.map((subItem) => (
                                            <Link
                                                key={subItem.title}
                                                href={subItem.href}
                                                className="flex justify-between items-center"
                                            >
                                                <span className="text-background/75">
                                                    {subItem.title}
                                                </span>
                                            </Link>
                                        ))}
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );
};
