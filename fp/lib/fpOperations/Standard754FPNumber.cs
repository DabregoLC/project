using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace fpOperations
{
	public class Standard754FPNumber
	{
		float floatVal_;
		decimal decimalVal_;

		string encodedHexString_;
		string ieee32BitString_;

		int encodedSign_;
		int encodedExponent_;
		int encodedMantissa_;

		double decodedSign_;
		double decodedExponent_;
		double decodedMantissa_;

		byte[] byteArray;

		public const string EMPTYSTR = "-9999";
		public const int EMPTYNUM = -9999;

		// constants for 32 bit words
		public static int NUM_EXPONENT_BITS32 = 8;
		public static int NUM_SIGN_BITS32 = 1;
		public static int NUM_MANTISSA_BITS32 = 23;
		public static int BIAS32 = 127;
		public static int HEX_LENGTH32 = 8;
		private void Init()
		{
			floatVal_ = EMPTYNUM;
			encodedHexString_ = EMPTYSTR;
			ieee32BitString_ = EMPTYSTR; ;
			decimalVal_ = EMPTYNUM;

			encodedSign_ = EMPTYNUM;
			encodedExponent_ = EMPTYNUM;
			encodedMantissa_ = EMPTYNUM; ;

			decodedSign_ = EMPTYNUM; ;
			decodedExponent_ = EMPTYNUM;
			decodedMantissa_ = EMPTYNUM;
		}

		public void Dump(string msg)
		{
			Debug.WriteLine("==========================================================");
			Debug.WriteLine("DUMP " + msg);
			Debug.WriteLine("use https://www.h-schmidt.net/FloatConverter/IEEE754.html to check\n");

			Debug.WriteLine("floatVal_         = {0} ", floatVal_);
			Debug.WriteLine("decimalVal_       = {0} ", decimalVal_);
			Debug.WriteLine("encodedHexString_ = " + encodedHexString_);
			Debug.WriteLine("ieee32String      = " + ieee32BitString_);
			Debug.WriteLine("encodedSign_      = {0} ", encodedSign_);
			Debug.WriteLine("encodedExponent_  = {0} ", encodedExponent_);
			Debug.WriteLine("encodedMantissa_  = {0} ", encodedMantissa_);
			Debug.WriteLine("decodedSign_      = {0} ", decodedSign_);
			Debug.WriteLine("decodedExponent_  = [{0}] 2^[{1}] = {2}", decodedExponent_, decodedExponent_, Math.Pow(2, decodedExponent_));
			Debug.WriteLine("decodedMantissa_  = {0} ", decodedMantissa_);
		}

		public Standard754FPNumber(float f1)
		{
			Init();
			floatVal_ = f1;

			// Convert to decimal 
			decimalVal_ = Convert.ToDecimal(BitConverter.ToSingle(BitConverter.GetBytes(floatVal_), 0));

			// Hex representation 
			byteArray = BitConverter.GetBytes(f1);
			Array.Reverse(byteArray);
			encodedHexString_ = BitConverter.ToString(byteArray).Replace("-", "");

			// ieee32 extract parts
			ieee32BitString_ = Standard754FPNumber.HexStringToBinaryString(encodedHexString_);
			string signStr32 = returnSignStr32();
			string expStr32 = returnExponentStr32();
			string mantissaStr32 = returnMantissaStr32();
			encodedSign_ = Convert.ToInt32(signStr32, 2);
			encodedExponent_ = Convert.ToInt32(expStr32, 2);
			encodedMantissa_ = Convert.ToInt32(mantissaStr32, 2);

			// decode sign bit, exponent
			decodedSign_ = (encodedSign_ > 0) ? -1 : 1;
			decodedExponent_ = encodedExponent_ - BIAS32;

			// decode mantissa part
			double fraction = 0.0;
			for (int i1 = 0; i1 < NUM_MANTISSA_BITS32; i1++)
			{
				double p1 = Math.Pow(2, ((i1 + 1) * -1));  // 2^1  2^2  2^3  				
				char c1 = mantissaStr32[i1];
				double b1 = (c1 == '1') ? 1 : 0;  // 1  0  1  1  0  0  1 as a number not a char
				double v1 = b1 * p1;  // 1*2^1+ 0*2^2 + 1*2^3 + 1*2^4 + 0*2^5 + 0 * 2*6 +...
				fraction += v1;
			}
			decodedMantissa_ = 1 + fraction;
		}

		public Standard754FPNumber(string signBitStr, string expBitsStr, string mantissaBitsStr)
		{
			Init();
			ieee32BitString_ = signBitStr + expBitsStr + mantissaBitsStr;

			encodedSign_ = Convert.ToInt32(signBitStr, 2);
			encodedExponent_ = Convert.ToInt32(expBitsStr, 2);
			encodedMantissa_ = Convert.ToInt32(mantissaBitsStr, 2);

			// to decode ieee 754
			// https://class.ece.iastate.edu/arun/Cpre305/ieee754/ie5.html	
			// (-1) ^ sign bit    *   (1 + fraction)   *  2^(exponent-bias)

			// (-1) ^ sign bit
			decodedSign_ = (encodedSign_ > 0) ? -1 : 1;

			// (1 + fraction)
			// example of fraction:
			// 0.10110011001100110011010bin = 1*2^1+ 0*2^2 + 1*2^3 + 1*2^4 + 0*2^5 + 0 * 2*6 +...			
			double fraction = 0.0;
			for (int i1 = 0; i1 < NUM_MANTISSA_BITS32; i1++)
			{
				double p1 = Math.Pow(2, ((i1 + 1) * -1));  // 2^1  2^2  2^3  				
				char c1 = mantissaBitsStr[i1];
				double b1 = (c1 == '1') ? 1 : 0;  // 1  0  1  1  0  0  1 as a number not a char
				double v1 = b1 * p1;  // 1*2^1+ 0*2^2 + 1*2^3 + 1*2^4 + 0*2^5 + 0 * 2*6 +...
				fraction += v1;
			}

			//  2^(exponent-bias)
			decodedExponent_ = encodedExponent_ - BIAS32;
			double decodedExponentVal = Math.Pow(2, decodedExponent_);

			// (-1) ^ sign bit    *   (1 + fraction)   *  2^(exponent-bias)				
			floatVal_ = (float)(decodedSign_ * (1 + fraction) * decodedExponentVal);
			decodedMantissa_ = (1 + fraction);
			decimalVal_ = (decimal)floatVal_;

			// Hex representation 
			byteArray = BitConverter.GetBytes(floatVal_);
			Array.Reverse(byteArray);
			encodedHexString_ = BitConverter.ToString(byteArray).Replace("-", "");
		}

		public string returnSignStr32()
		{
			return ieee32BitString_.Substring(0, 1);
		}

		public string returnExponentStr32()
		{
			return ieee32BitString_.Substring(1, 8);
		}

		public string returnMantissaStr32()
		{
			return ieee32BitString_.Substring(9, 23);
		}

		public string returnEncodedHexString()
		{
			return encodedHexString_;
		}

		public decimal returnDecimalVal()
		{
			return decimalVal_;
		}
		public float returnFloatVal()
		{
			return floatVal_;
		}

		private static readonly Dictionary<char, string> hexCharacterToBinaryDict = new Dictionary<char, string> {
		{'0', "0000"}, {'1', "0001"}, {'2', "0010"}, {'3', "0011"}, {'4', "0100"}, {'5', "0101"}, {'6', "0110"}, {'7', "0111"}, {'8', "1000"}, {'9', "1001"}, {'a', "1010"}, {'b', "1011"}, {'c', "1100"}, {'d', "1101"}, {'e', "1110"}, {'f', "1111"}
	};
		public static string HexStringToBinaryString(string hexStr)
		{
			string validHexChars = "";
			foreach (var item in hexCharacterToBinaryDict)
			{
				validHexChars = validHexChars + Char.ToUpper(item.Key);
			}

			System.Text.StringBuilder result = new System.Text.StringBuilder();
			foreach (char c1 in hexStr)
			{
				if (validHexChars.Contains(c1.ToString()))
				{
					result.Append(hexCharacterToBinaryDict[char.ToLower(c1)]);
				}
				else
				{
					Console.WriteLine("INVALID HEX {0} ", c1);
				}
			}

			return result.ToString();
		}

		public static float HexStringToFloat(string hexastr)
		{
			int len = hexastr.Length;
			float fn;

			if (len != HEX_LENGTH32)
			{
				Console.WriteLine("ERROR HexStringToFloat LEN == {0}", len);
				fn = EMPTYNUM;
			}
			else
			{
				Console.WriteLine("HexStringToFloat LEN == {0}", len);
				UInt32 number1 = Convert.ToUInt32(hexastr, 16);
				byte[] floatvalue = BitConverter.GetBytes(number1);
				fn = BitConverter.ToSingle(floatvalue, 0);
			}
			return fn;
		}

		public bool IsRealNumber(int sign, int exp, int mantissa)
		{
			return true;
		}

		float add(decimal d1)
		{
			return 1.1F;
		}

		public static Standard754FPNumber operator +(Standard754FPNumber b1, Standard754FPNumber c1)
		{
			float f1 = b1.returnFloatVal() + c1.returnFloatVal();
			Standard754FPNumber fpn = new Standard754FPNumber(f1);
			return fpn;
		}

		public static Standard754FPNumber operator -(Standard754FPNumber b1, Standard754FPNumber c1)
		{
			float f1 = b1.returnFloatVal() - c1.returnFloatVal();
			Standard754FPNumber fpn = new Standard754FPNumber(f1);
			return fpn;
		}

		public static Standard754FPNumber operator *(Standard754FPNumber b1, Standard754FPNumber c1)
		{
			float f1 = b1.returnFloatVal() * c1.returnFloatVal();
			Standard754FPNumber fpn = new Standard754FPNumber(f1);
			return fpn;
		}


	}
}
