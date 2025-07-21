using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyConsoleApp.Pages.Calculator
{
    public class IndexModel : PageModel
    {
        public double Op1 { get; set; }
        public double Op2 { get; set; }
        public string Opr { get; set; } = "+";
        public string Func { get; set; } = "sin";
        public double Value { get; set; }
        public string Const { get; set; } = "";
        
        public double ResultValue { get; set; }
        public bool HasResult { get; set; } = false;
        public string ErrorMessage { get; set; } = "";

        public void OnGet(double op1 = 0, double op2 = 0, string opr = "+", string func = "sin", double value = 0, string constParam = "")
        {
            Op1 = op1;
            Op2 = op2;
            Opr = opr;
            Func = func;
            Value = value;
            Const = constParam;

            try
            {
                if (!string.IsNullOrEmpty(Const))
                {
                    // 수학 상수 처리
                    switch (Const.ToLower())
                    {
                        case "pi":
                            ResultValue = Math.PI;
                            HasResult = true;
                            break;
                        case "e":
                            ResultValue = Math.E;
                            HasResult = true;
                            break;
                        case "sqrt2":
                            ResultValue = Math.Sqrt(2);
                            HasResult = true;
                            break;
                        case "phi":
                            ResultValue = (1 + Math.Sqrt(5)) / 2; // 황금비
                            HasResult = true;
                            break;
                    }
                }
                else if (!string.IsNullOrEmpty(Func) && Value != 0)
                {
                    // 고급 수학 함수 처리
                    switch (Func.ToLower())
                    {
                        case "sin":
                            ResultValue = Math.Sin(Value * Math.PI / 180); // 도 단위를 라디안으로 변환
                            HasResult = true;
                            break;
                        case "cos":
                            ResultValue = Math.Cos(Value * Math.PI / 180);
                            HasResult = true;
                            break;
                        case "tan":
                            ResultValue = Math.Tan(Value * Math.PI / 180);
                            HasResult = true;
                            break;
                        case "log":
                            if (Value > 0)
                            {
                                ResultValue = Math.Log10(Value);
                                HasResult = true;
                            }
                            else
                            {
                                ErrorMessage = "로그 함수는 양수에 대해서만 계산할 수 있습니다.";
                            }
                            break;
                        case "ln":
                            if (Value > 0)
                            {
                                ResultValue = Math.Log(Value);
                                HasResult = true;
                            }
                            else
                            {
                                ErrorMessage = "자연로그 함수는 양수에 대해서만 계산할 수 있습니다.";
                            }
                            break;
                        case "sqrt":
                            if (Value >= 0)
                            {
                                ResultValue = Math.Sqrt(Value);
                                HasResult = true;
                            }
                            else
                            {
                                ErrorMessage = "제곱근은 음수에 대해 계산할 수 없습니다.";
                            }
                            break;
                        case "abs":
                            ResultValue = Math.Abs(Value);
                            HasResult = true;
                            break;
                        case "exp":
                            ResultValue = Math.Exp(Value);
                            HasResult = true;
                            break;
                    }
                }
                else if (Op1 != 0 || Op2 != 0 || !string.IsNullOrEmpty(Opr))
                {
                    // 기본 계산 처리
                    switch (Opr)
                    {
                        case "+":
                            ResultValue = Op1 + Op2;
                            HasResult = true;
                            break;
                        case "-":
                            ResultValue = Op1 - Op2;
                            HasResult = true;
                            break;
                        case "*":
                            ResultValue = Op1 * Op2;
                            HasResult = true;
                            break;
                        case "/":
                            if (Op2 != 0)
                            {
                                ResultValue = Op1 / Op2;
                                HasResult = true;
                            }
                            else
                            {
                                ErrorMessage = "0으로 나눌 수 없습니다.";
                            }
                            break;
                        case "%":
                            if (Op2 != 0)
                            {
                                ResultValue = Op1 % Op2;
                                HasResult = true;
                            }
                            else
                            {
                                ErrorMessage = "0으로 나머지 연산을 할 수 없습니다.";
                            }
                            break;
                        case "**":
                            ResultValue = Math.Pow(Op1, Op2);
                            HasResult = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"계산 중 오류가 발생했습니다: {ex.Message}";
                HasResult = false;
            }
        }
    }
}
