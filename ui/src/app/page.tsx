import Link from 'next/link'

export default function Home() {
  return (
    <div>
      <h1>Home</h1>
      <Link href="/guardians"><p>Guardians</p></Link>
      <Link href="/tutees"><p>Tutees</p></Link>
    </div>
  );
}
