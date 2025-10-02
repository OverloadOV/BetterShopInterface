using Sandbox.Game.Entities.Blocks;
using Sandbox.Graphics.GUI;
using System.Collections.Generic;
using VRage.Utils;
using VRageMath;

namespace ClientPlugin.Shop
{
    internal class CustomShopScreen : MyGuiScreenBase
    {
        private readonly List<MyStoreItem> storeItems;
        private readonly long lastEconomyTick;
        private readonly float offersBonus;
        private readonly float ordersBonus;

        public CustomShopScreen(List<MyStoreItem> storeItems, long lastEconomyTick, float offersBonus, float ordersBonus, Vector2 position, Vector2 size)
            : base(position, MyGuiConstants.SCREEN_BACKGROUND_COLOR, size)
        {
            this.storeItems = storeItems;
            this.lastEconomyTick = lastEconomyTick;
            this.offersBonus = offersBonus;
            this.ordersBonus = ordersBonus;
            CloseButtonEnabled = true;
            EnabledBackgroundFade = true;
        }

        public override string GetFriendlyName() => "Better Store Interface";

        public override void LoadContent()
        {
            // Overwrite screen content
            base.LoadContent();
            CreateShopControls();
        }

        private void CreateShopControls()
        {
            float yOffset = -Size.GetValueOrDefault().Y / 2 + 0.05f;
            float verticalSpacing = 0.08f;

            foreach (var item in storeItems)
            {
                // Panel per article
                var panel = new MyGuiControlParent
                {
                    Position = new Vector2(0, yOffset),
                    Size = new Vector2(Size.GetValueOrDefault().X - 0.1f, verticalSpacing),
                    BackgroundTexture = MyGuiConstants.TEXTURE_RECTANGLE_DARK
                };

                // Label
                var label = new MyGuiControlLabel
                {
                    Text = $"ID: {item.Id}, Price: {item.PricePerUnit}, Amount: {item.Amount}",
                    OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_CENTER,
                    Position = new Vector2(-panel.Size.X / 2 + 0.02f, 0)
                };
                panel.Controls.Add(label);

                // Buy
                var buyButton = new MyGuiControlButton(
                    text: new System.Text.StringBuilder("Buy"),
                    position: new Vector2(panel.Size.X / 2 - 0.06f, 0),
                    size: new Vector2(0.12f, 0.05f),
                    onButtonClick: btn => OnBuyClicked(item),
                    originAlign: MyGuiDrawAlignEnum.HORISONTAL_RIGHT_AND_VERTICAL_CENTER);
                panel.Controls.Add(buyButton);

                Controls.Add(panel);

                yOffset += verticalSpacing + 0.02f;
            }
        }

        private void OnBuyClicked(MyStoreItem item)
        {

            MyGuiSandbox.AddScreen(MyGuiSandbox.CreateMessageBox(
                MyMessageBoxStyleEnum.Info,
                buttonType: MyMessageBoxButtonsType.OK,
                messageText: new System.Text.StringBuilder($"Buy All: {item.Id}"),
                messageCaption: new System.Text.StringBuilder("Buy"),
                size: new Vector2(0.3f, 0.2f)
            ));
        }
    }
}