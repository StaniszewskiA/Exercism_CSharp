using System;
using System.Numerics;

public static class DiffieHellman
{
    public static BigInteger PrivateKey(BigInteger primeP) 
    {
        Random rnd = new Random();
        BigInteger privateKey;
        do
        {
            byte[] bytes = new byte[primeP.ToByteArray().Length];
            rnd.NextBytes(bytes);
            privateKey = new BigInteger(bytes);
        } while (privateKey <= 1 || privateKey >= primeP - 1);

        return privateKey;
    }

    public static BigInteger PublicKey(BigInteger primeP, BigInteger primeG, BigInteger privateKey) => BigInteger.ModPow(primeG, privateKey, primeP);

    public static BigInteger Secret(BigInteger primeP, BigInteger publicKey, BigInteger privateKey) => BigInteger.ModPow(publicKey, privateKey, primeP);
}