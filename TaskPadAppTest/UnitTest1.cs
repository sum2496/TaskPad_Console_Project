using Microsoft.VisualStudio.TestPlatform.TestHost;
using TaskPadApp;

namespace TaskPadAppTest
{
    public class Tests
    {
        [Test]
        public void UserShouldOnlyGiveAlphaNumericCharacters()
        {
            string input1 = "1";
            string input2 = "as";
            string input3 = "er23";
            string input4 = "ertrt45;[]";

            bool actinput1 = TaskPadApp.Program.IsAlphanumeric(input1);
            bool actinput2 = TaskPadApp.Program.IsAlphanumeric(input2);
            bool actinput3 = TaskPadApp.Program.IsAlphanumeric(input3);
            bool actinput4 = TaskPadApp.Program.IsAlphanumeric(input4);
            Assert.Multiple(() =>
            {
                Assert.That(actinput1, Is.EqualTo(true));
                Assert.That(actinput2, Is.EqualTo(true));
                Assert.That(actinput3, Is.EqualTo(true));
                Assert.That(actinput4, Is.EqualTo(false));
            });
        }

        [Test]
        public void UserEnterChoiceBetween1And8ShouldbeValid([Values("1","2","8")] string input)
        {
            bool actualinput = TaskPadApp.Program.ValidUserChoiceFromTable(input);

            Assert.That(actualinput, Is.EqualTo(true));
        }

        [Test]
        public void UserEnterInvalidChoiceShouldbeInValid([Values("", "abdf")] string input)
        {
            bool actualinput = TaskPadApp.Program.ValidUserChoiceFromTable(input);

            Assert.That(actualinput, Is.EqualTo(false));
        }

        [Test]
        public void UserToContinueTaskPadAppEntersOtherThan0Or1ReturnInvalid([Values("2","", "abdf")] string input)
        {
            bool actualinput = TaskPadApp.Program.InvalidChoiceForContinue(input);

            Assert.That(actualinput, Is.EqualTo(true));
        }
    }
}