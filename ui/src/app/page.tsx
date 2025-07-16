import Link from 'next/link'

export default function Home() {
  return (
    <div>
      <Link href="/guardians"><p>Guardians</p></Link>
      <Link href="/tutees"><p>Tutees</p></Link>
    </div>
  );
}
