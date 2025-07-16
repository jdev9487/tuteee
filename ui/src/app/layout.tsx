import type { Metadata } from "next";
import Link from "next/link";

export const metadata: Metadata = {
  title: "asdf",
  description: "qwer"
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Link href="/"><h1>Home</h1></Link>
        {children}
      </body>
    </html>
  );
}
